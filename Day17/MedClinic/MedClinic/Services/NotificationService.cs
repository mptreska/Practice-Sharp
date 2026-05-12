using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedClinic.Services
{
    public class NotificationService
    {
        private const string MapName = "MedClinicNotif";
        private const int MapSize = 1024;
        private CancellationTokenSource cts;

        public event Action<string> NotificationReceived;

        public void SendNotification(string message)
        {
            try
            {
                using (var mmf = MemoryMappedFile.CreateOrOpen(MapName, MapSize))
                using (var acc = mmf.CreateViewAccessor())
                {
                    byte[] data = new byte[MapSize];
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    Array.Copy(msg, data, Math.Min(msg.Length, MapSize - 1));
                    acc.WriteArray(0, data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка уведомления: {ex.Message}");
            }
        }

        public void StartListening()
        {
            cts = new CancellationTokenSource();
            Task.Run(() => ListenLoop(cts.Token));
        }

        private async Task ListenLoop(CancellationToken token)
        {
            string last = "";
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var mmf = MemoryMappedFile.OpenExisting(MapName))
                    using (var acc = mmf.CreateViewAccessor())
                    {
                        byte[] data = new byte[MapSize];
                        acc.ReadArray(0, data, 0, data.Length);
                        string current = Encoding.UTF8
                            .GetString(data).TrimEnd('\0').Trim();

                        if (!string.IsNullOrEmpty(current) && current != last)
                        {
                            last = current;
                            NotificationReceived?.Invoke(current);
                        }
                    }
                }
                catch { }

                await Task.Delay(1000, token);
            }
        }

        public void Stop() => cts?.Cancel();
    }
}