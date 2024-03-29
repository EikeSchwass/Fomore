﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor.Behaviours;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Tools
{
    public class PanTool : Tool
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.HandGrabOutline, BaseBehaviour.BehaviourBrush);

        /// <inheritdoc />
        public override ToolType ToolType { get; } = ToolType.PanningTool;

        private Point StartPosition { get; set; }
        protected bool IsDragging { get; private set; }

        /// <inheritdoc />
        public override bool OnCanvasMouseMove(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            if (!IsDragging) return true;

            double deltaX = (mouseInfo.AbosultePosition - StartPosition).X;
            double deltaY = (mouseInfo.AbosultePosition - StartPosition).Y;
            StartPosition = mouseInfo.AbosultePosition;

            canvasVM.CameraVM.OffsetX += deltaX;
            canvasVM.CameraVM.OffsetY += deltaY;

            return true;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseDown(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            IsDragging = true;
            StartPosition = mouseInfo.AbosultePosition;

            return false;
        }

        /// <inheritdoc />
        public override bool OnCanvasMouseUp(MouseInfo mouseInfo, CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            IsDragging = false;
            return false;
        }

        /// <inheritdoc />
        public override void OnCanvasMouseLeave(CreatureStructureEditorCanvasVM canvasVM, ModifierKeys modifierKeys)
        {
            OnCanvasMouseUp(default(MouseInfo), canvasVM, modifierKeys);
        }

        /// <inheritdoc />
        public override InputGesture InputGesture { get; } = new KeyGesture(Key.Space);
    }
}