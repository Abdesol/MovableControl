using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Media;
using MouseButton = Avalonia.Remote.Protocol.Input.MouseButton;

namespace MovableControl.Controls
{
    public class CustomMovableBorder : Border
    {
        private bool _isPressed; 
        private Point _positionInBlock;
        private TranslateTransform _transform = null!;

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            _isPressed = true;
            _positionInBlock = e.GetPosition(Parent);
            
            if (_transform != null!) 
                _positionInBlock = new Point(
                    _positionInBlock.X - _transform.X,
                    _positionInBlock.Y - _transform.Y);
            
            base.OnPointerPressed(e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _isPressed = false;
            
            base.OnPointerReleased(e);
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            if (!_isPressed)
                return;
            
            if (Parent == null)
                return;

            var currentPosition = e.GetPosition(Parent);

            var offsetX = currentPosition.X -  _positionInBlock.X;
            var offsetY = currentPosition.Y - _positionInBlock.Y;

            _transform = new TranslateTransform(offsetX, offsetY);
            RenderTransform = _transform;
            
            base.OnPointerMoved(e);
        }
    }
}