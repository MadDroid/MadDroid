using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace MadDroid.UWP.Attached
{
    /// <summary>
    /// Attached properties for <see cref="ProgressBar"/>
    /// </summary>
    public class ProgressBarAttached
    {
        /// <summary>
        /// Get the value to be animated
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetAnimatedValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(AnimatedValueProperty);
        }

        /// <summary>
        /// Set the value to be animated
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAnimatedValue(DependencyObject obj, bool value)
        {
            obj.SetValue(AnimatedValueProperty, value);
        }

        /// <summary>
        /// Animate the value of an <see cref="ProgressBar"/>
        /// </summary>
        public static readonly DependencyProperty AnimatedValueProperty =
            DependencyProperty.RegisterAttached("AnimatedValue", typeof(bool), typeof(ProgressBarAttached), new PropertyMetadata(0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Verify if the dependency property is ProgressBar and the new value is double
            if (d is RangeBase progressBar && e.NewValue is double value)
            {
                // Create a new story board
                var storyBoard = new Storyboard();

                // Create a new double animation
                var doubleAnimation = new DoubleAnimationUsingKeyFrames
                {
                    EnableDependentAnimation = true
                };

                // Crate a new easing keyframe
                var easingKeyFrame = new EasingDoubleKeyFrame
                {
                    Value = value,
                    KeyTime = TimeSpan.FromMilliseconds(250),
                    EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseInOut }
                };

                // Add the keyframe to the double animation
                doubleAnimation.KeyFrames.Add(easingKeyFrame);

                // Add the double animation to the storyboard
                storyBoard.Children.Add(doubleAnimation);

                // Set the target
                Storyboard.SetTarget(doubleAnimation, progressBar);
                // Set the property
                Storyboard.SetTargetProperty(doubleAnimation, "value");

                // Start the storyboard
                storyBoard.Begin();
            }
        }
    }
}
