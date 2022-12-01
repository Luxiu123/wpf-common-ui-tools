﻿using CommonUITools.Utils;
using ModernWpf.Controls;
using System.Windows;

namespace CommonUITools.View;

public class BaseDialog : ContentDialog {
    public static readonly DependencyProperty DetailTextProperty = DependencyProperty.Register("DetailText", typeof(string), typeof(BaseDialog), new PropertyMetadata(""));
    /// <summary>
    /// DetailText
    /// </summary>
    public string DetailText {
        get { return (string)GetValue(DetailTextProperty); }
        set { SetValue(DetailTextProperty, value); }
    }

    public BaseDialog(
        string title = "",
        string detailText = "",
        string okButtonText = "确定",
        string cancelButtonText = "关闭"
    ) {
        DataContext = this;
        Title = title;
        DetailText = detailText;
        PrimaryButtonText = okButtonText;
        CloseButtonText = cancelButtonText;
        //Init();
        if (TryFindResource("GlobalDefaultButtonStyle") is Style closeButtonStyle) {
            CloseButtonStyle = closeButtonStyle;
        }
        if (TryFindResource("BaseDialogDataTemplate") is DataTemplate titleDataTemplate) {
            TitleTemplate = titleDataTemplate;
        }
        // 设置 ScaleAnimation
        Opened += (dialog, _) => TaskUtils.EnsureCalledOnce((dialog, Application.Current), () => {
            Utils.ScaleAnimationHelper.SetIsEnabled((DependencyObject)dialog.Content, true);
            Utils.ScaleAnimationHelper.SetScaleOption((DependencyObject)dialog.Content, Utils.ScaleAnimationHelper.ScaleOption.Center);
        });
    }

    public BaseDialog() : this("") { }
}

public class DialogResult<T> {
    public ContentDialogResult Result { get; set; }
    public T? Data { get; set; } = default;

    public override string ToString() {
        return $"{nameof(Result)}: {Result}; {nameof(Data)}: {Data}";
    }
}
