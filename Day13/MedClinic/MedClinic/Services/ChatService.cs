using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedClinic.Services
{
    public class ChatService
    {
        private const string PipeName = "MedClinicChatPipe";
        private CancellationTokenSource cts;

        public event Action<string> MessageReceived;

        public void StartListening()
        {
            cts = new CancellationTokenSource();
            Task.Run(() => ListenLoop(cts.Token));
        }

        private async Task ListenLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var server = new NamedPipeServerStream(
                        PipeName,
                        PipeDirection.In,
                        NamedPipeServerStream.MaxAllowedServerInstances))
                    {
                        await server.WaitForConnectionAsync(token);
                        using (var reader = new StreamReader(server, Encoding.UTF8))
                        {
                            string msg = await reader.ReadToEndAsync();
                            if (!string.IsNullOrEmpty(msg))
                                MessageReceived?.Invoke(msg);
                        }
                    }
                }
                catch (OperationCanceledException) { break; }
                catch { await Task.Delay(500); }
            }
        }

        public async Task SendMessageAsync(string senderName, string text)
        {
            try
            {
                using (var client = new NamedPipeClientStream(
                    ".", PipeName, PipeDirection.Out))
                {
                    await client.ConnectAsync(2000);
                    string full = $"[{DateTime.Now:HH:mm}] {senderName}: {text}";
                    using (var writer = new StreamWriter(client, Encoding.UTF8))
                    {
                        writer.AutoFlush = true;
                        await writer.WriteAsync(full);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke($"[Ошибка]: {ex.Message}");
            }
        }

        public void Stop() => cts?.Cancel();
    }
}