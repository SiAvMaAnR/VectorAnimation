using NewVectorAnimation.ViewModels;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewVectorAnimation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;
        private Storyboard storyboard;


        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            storyboard = new Storyboard();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.BeginInvoke(() =>
            {
                DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath()
                {
                    PathGeometry = this.WavePath.Data.GetFlattenedPathGeometry(),
                    Source = PathAnimationSource.X,
                    Duration = new Duration(TimeSpan.FromSeconds(20)),
                    IsAdditive = true,
                    IsCumulative = true
                };

                DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath()
                {
                    PathGeometry = this.WavePath.Data.GetFlattenedPathGeometry(),
                    Source = PathAnimationSource.Y,
                    Duration = new Duration(TimeSpan.FromSeconds(20)),
                };

                DoubleAnimationUsingPath animationAngle = new DoubleAnimationUsingPath()
                {
                    PathGeometry = this.WavePath.Data.GetFlattenedPathGeometry(),
                    Source = PathAnimationSource.Angle,
                    Duration = new Duration(TimeSpan.FromSeconds(20)),
                };

                Storyboard.SetTargetName(animationX, "ShipPath");
                Storyboard.SetTargetName(animationY, "ShipPath");
                Storyboard.SetTargetName(animationAngle, "ShipPath");

                Storyboard.SetTargetProperty(animationX, new PropertyPath("(Canvas.Left)"));
                Storyboard.SetTargetProperty(animationY, new PropertyPath("(Canvas.Top)"));
                Storyboard.SetTargetProperty(animationAngle, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


                storyboard.Children.Add(animationX);
                storyboard.Children.Add(animationY);
                storyboard.Children.Add(animationAngle);

                storyboard.Begin(this, true);


            });

        }
    }
}
