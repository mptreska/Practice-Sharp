using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace MedClinic
{
    public static class AnimationHelper
    {
        // Плавное появление элемента
        public static void FadeIn(UIElement element, double durationSeconds = 0.5)
        {
            element.Opacity = 0;
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(durationSeconds)
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        // Плавное исчезновение элемента
        public static void FadeOut(UIElement element, double durationSeconds = 0.3,
                                   Action onComplete = null)
        {
            var animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(durationSeconds)
            };
            if (onComplete != null)
                animation.Completed += (s, e) => onComplete();

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        // Анимация высоты (раскрытие)
        public static void ExpandHeight(FrameworkElement element,
                                        double fromHeight, double toHeight,
                                        double durationSeconds = 0.3)
        {
            var animation = new DoubleAnimation
            {
                From = fromHeight,
                To = toHeight,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            element.BeginAnimation(FrameworkElement.HeightProperty, animation);
        }

        // Анимация сдвига (выезд слева)
        public static void SlideInFromLeft(UIElement element,
                                           double durationSeconds = 0.4)
        {
            var transform = new System.Windows.Media.TranslateTransform(-300, 0);
            element.RenderTransform = transform;

            var animation = new DoubleAnimation
            {
                From = -300,
                To = 0,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            transform.BeginAnimation(
                System.Windows.Media.TranslateTransform.XProperty, animation);
        }

        // Пульсация (для важных записей)
        public static void Pulse(UIElement element)
        {
            var animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.6,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3)
            };
            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}