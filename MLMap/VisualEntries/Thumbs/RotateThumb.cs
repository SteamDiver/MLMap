using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DiagramDesigner
{
    public class RotateThumb : Thumb
    {
        private Point centerPoint;
        private Vector startVector;
        private double initialAngle;
        private Canvas designerCanvas;
        private ContentControl designerItem;
        private RotateTransform rotateTransform;

        public RotateThumb()
        {
            DragDelta += RotateThumb_DragDelta;
            DragStarted += RotateThumb_DragStarted;
        }

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            designerItem = DataContext as ContentControl;

            if (designerItem != null)
            {
                designerCanvas = (Canvas) VisualTreeHelper.GetParent(designerItem);

                if (designerCanvas != null)
                {
                    centerPoint = designerItem.TranslatePoint(
                        new Point(designerItem.Width * designerItem.RenderTransformOrigin.X,
                                  designerItem.Height * designerItem.RenderTransformOrigin.Y),
                                  designerCanvas);

                    Point startPoint = Mouse.GetPosition(designerCanvas);
                    startVector = Point.Subtract(startPoint, centerPoint);

                    rotateTransform = designerItem.RenderTransform as RotateTransform;
                    if (rotateTransform == null)
                    {
                        designerItem.RenderTransform = new RotateTransform(0);
                        initialAngle = 0;
                    }
                    else
                    {
                        initialAngle = rotateTransform.Angle;
                    }
                }
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (designerItem != null && designerCanvas != null)
            {
                Point currentPoint = Mouse.GetPosition(designerCanvas);
                Vector deltaVector = Point.Subtract(currentPoint, centerPoint);

                double angle = Vector.AngleBetween(startVector, deltaVector);

                RotateTransform rotateTransform = designerItem.RenderTransform as RotateTransform;
                rotateTransform.Angle = initialAngle + Math.Round(angle, 0);
                designerItem.InvalidateMeasure();
            }
        }
    }
}
