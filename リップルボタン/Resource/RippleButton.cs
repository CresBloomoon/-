using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace リップルボタン.Resource {
    internal sealed class RippleButton : Button{
        
        public Brush RippleColor {
            get => (Brush)GetValue(RippleColorProperty);
            set => SetValue(RippleColorProperty, value);
        }

        public static readonly DependencyProperty RippleColorProperty =
            DependencyProperty.Register("RippleColor", typeof(Brush),
                typeof(RippleButton), new PropertyMetadata(Brushes.White));

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            this.AddHandler(MouseDownEvent, new RoutedEventHandler(this.OnMouseDown), true);
        }

        public void OnMouseDown(object sender, RoutedEventArgs e) {
            Point mousePos = (e as MouseButtonEventArgs).GetPosition(this);

            var ellipse = GetTemplateChild("CircleEffect") as Ellipse;

            ellipse.Margin = new Thickness(mousePos.X, mousePos.Y, 0, 0);

            Storyboard storyboard = (FindResource("RippleAnimation") as Storyboard).Clone();

            double effectMaxSize = Math.Max(ActualWidth, ActualHeight) * 3;

            (storyboard.Children[2] as ThicknessAnimation).From = new Thickness(mousePos.X, mousePos.Y, 0, 0);
            (storyboard.Children[2] as ThicknessAnimation).To = new Thickness(mousePos.X - effectMaxSize / 2, mousePos.Y - effectMaxSize / 2, 0, 0);
            (storyboard.Children[3] as DoubleAnimation).To = effectMaxSize;

            ellipse.BeginStoryboard(storyboard);
        }
    }
}
