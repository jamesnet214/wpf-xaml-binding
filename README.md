# wpfxamlbinding
> ncoresoftgit [here.](https://github.com/ncoresoftsource/ncoresoftgit)   
We hope you also refer to this article for better understanding. [here.](https://github.com/ncoresoftsource/trigger)
## 1. Overview
- DataContext
- Binding
- Binding Element
- MultiBinding
- Self Property Binding
- TemplatedParent Binding
- Static Property Binding

## 2. DataContext
> DataContext is object-type Property. And this is included in the FrameworkElement class. And given that all of the WPF's UI objects have DataContext Properties, we can imagine all of these objects inheriting the FrameworkElement somewhere in the top. And I assure you, just knowing this existence of FrameworkElement is a big help to you in the future to take Binding behavior in WPF more meaningfully, so I mentioned it before I started.   

Inheriting `FrameworkElement` of WPF UI Control Classis
```csharp
TextBox : FrameworkElement
TextBlock : FrmaeworkElement
Button : FrameworkElement
Window : FrameworkElement
UserControl : FrameworkElement
...
```
And, All control exists `DataContext`
```
// myTextBox.DataContext
// myButton.DataContext
// myWindow.DataContext
...
```
### Binding
string property
```xaml
<TextBox Text="{Binding Keywords}"/>
```

### Binding Element
```xaml
<CheckBox x:Name="ckUseEmail"/>
<TextBlock Text="{Binding ElementName=ckUseEmail, Path=IsChecked}"/>
```
### MultiBinding
```xaml
<TextBlock Margin="5,2" Text="This dissappears as the control gets focus...">
    <TextBlock.Visibility>
        <MultiBinding Converter="{StaticResource TextInputToVisibilityConverter}">
            <Binding ElementName="txtUserEntry2" Path="Text.IsEmpty" />
            <Binding ElementName="txtUserEntry2" Path="IsFocused" />
        </MultiBinding>
    </TextBlock.Visibility>
</TextBlock>
```
### Self Property Binding
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Tag}"/>
```
Truly, same with this code.
```xaml
<TextBlock x:Name="txt" Text="{Binding ElementName=txt, Path=Tag}"/>
```
Yes. You no longer have to declare `x:Name` to bind your own property.
### Binding (Find Parent)
Imports based on the parent control closest to it.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}"/>
```
In addition to the properties of the controls found, the properties within the DataContext object can be used if it exists.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.Email}"/>
```

### Static Property Binding
You can access binding property value directly.   
First, declare `static` property.
```csharp
namespace Exam
{
    public class ExamClass
    {
        public static string ExamText { get; set; }
    }
} 
```

Second, using static class in XAML.
```xaml
<Window ... xmlns:exam="clr-namespace:Exam">
```

Third, just binding property.
```xaml
<TextBlock Text="{Binding exam:ExamClass.ExamText}"/>
```

Or, you can set Resource key like using `Converter`.
```xaml
<Window.Resource>
    <exam:ExamClass x:Key="ExamClass">
</Window.Resource>
...

<TextBlock Text="{Binding Source={StaticResource ExamClass}, Path=ExamText}"/>
```
> I have never used the Static Property under normal circumstances. This is because data that deviates from its own DataContext can disrupt the flow of whole WPF applications and impair readability significantly. However, this method is actively used in the development stage to implement fast testing and functions, as well as in the DataContext (or ViewModel).
