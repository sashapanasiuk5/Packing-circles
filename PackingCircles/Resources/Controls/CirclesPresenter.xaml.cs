using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PackingCircles.Models;
using PackingCircles.ViewModels;

namespace PackingCircles.Resources.Controls;

public partial class CirclesPresenter : UserControl
{
    public static readonly DependencyProperty CirclesToShowProperty = DependencyProperty.Register(
        nameof(CirclesToShow), typeof(ObservableCollection<Circle>), typeof(CirclesPresenter), new PropertyMetadata(default(ObservableCollection<Circle>), CirclesChanged));

    private static void CirclesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((CirclesPresenter)d).DrawCircles();
    }

    public ObservableCollection<Circle> CirclesToShow
    {
        get { return (ObservableCollection<Circle>)GetValue(CirclesToShowProperty); }
        set { SetValue(CirclesToShowProperty, value); }
    }
    
    
    public CirclesPresenter()
    {
        InitializeComponent();
    }

    private void DrawCircles()
    {
        MainCanvas.Children.Clear();
        foreach (var circle in CirclesToShow)
        {
            Ellipse ellipse = new Ellipse() { Width = circle.Radius*2, Height = circle.Radius*2};
            ellipse.Stroke = new SolidColorBrush(Color.FromRgb(229, 28, 28));
            ellipse.StrokeThickness = 0.5;
            Canvas.SetTop(ellipse, 250 - circle.Position.Y - circle.Radius);
            Canvas.SetLeft(ellipse, 250 + circle.Position.X  - circle.Radius);
            MainCanvas.Children.Add(ellipse);
        }

        Ellipse mark = new Ellipse() { Width = 5, Height = 5, Fill = Brushes.Blue };
        Canvas.SetTop(mark, 250);
        Canvas.SetLeft(mark, 250);
        MainCanvas.Children.Add(mark);
        DrawOuterCircle();
    }

    private void DrawOuterCircle()
    {
        double maxDistance = 0;
        foreach (var circle in CirclesToShow)
        {
            double distance = Math.Sqrt(Math.Pow(circle.Position.X, 2) + Math.Pow(circle.Position.Y, 2)) + circle.Radius;
            if (distance > maxDistance) {
                maxDistance = distance;
            }
        }

        double radius = maxDistance;
        Ellipse outerCircle = new Ellipse(){Width = radius*2, Height = radius*2, Stroke = Brushes.Blue, StrokeThickness = 1};
        Canvas.SetTop(outerCircle, 250 - radius);
        Canvas.SetLeft(outerCircle, 250 - radius);
        MainCanvas.Children.Add(outerCircle);
    }
}