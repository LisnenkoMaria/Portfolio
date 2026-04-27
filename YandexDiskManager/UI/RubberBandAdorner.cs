using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.ComponentModel;

namespace YandexDiskManager.UI 
{
    public class RubberBandAdorner : Adorner
    {
        private Point _startPoint;
        private Point _endPoint;
        private Pen _rubberBandPen;
        private Brush _rubberBandBrush;
        private string hexColor = "#FFCC00";
     
        public RubberBandAdorner(UIElement adornedElement, Point startPoint) : base(adornedElement)
        {
            
            Color color = (Color) ColorConverter.ConvertFromString(hexColor);

            _startPoint = startPoint;
            _endPoint = startPoint;

            // Сплошная синяя рамка
            _rubberBandPen = new Pen(new SolidColorBrush(color), 1);

            // Полупрозрачная голубая заливка
            _rubberBandBrush = new SolidColorBrush(color) { Opacity = 0.2 };
        }

        /// <summary>
        /// Обновляет конечную точку и перерисовывает прямоугольник
        /// </summary>
        public void UpdateSelection(Point endPoint)
        {
            _endPoint = endPoint;
            InvalidateVisual(); // Говорим WPF, что нужно перерисовать
        }

        /// <summary>
        /// Возвращает финальный прямоугольник выделения
        /// </summary>
        public Rect GetSelectionRect()
        {
            return new Rect(_startPoint, _endPoint);
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            // Рисуем прямоугольник от _startPoint до _endPoint
            dc.DrawRectangle(_rubberBandBrush, _rubberBandPen, GetSelectionRect());
        }
    }
}