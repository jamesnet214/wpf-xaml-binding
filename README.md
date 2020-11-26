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
DataContext is the DependencyProperty Property included in the FrameworkElement. `PresentationFramework.dll`
```csharp
namespace System.Windows
{
    public class FrameworkElement : UIElement
    {
        public static readonly DependencyProperty DataContextProperty;
        public object DataContext { get; set; }
    }
}
```
And, All ui controls in wpf inherit the FrameworkElement class.   
> At this point in learning Binding or DataContext, there is no need to study and study FrameworkElement in greater depth. However, this is to briefly mention the fact that the closest object that can encompass all UI controls is the FrameworkElement.   

### DataContext is always the reference point for Binding.
Binding can directly recall values for the DataContext type format starting with the nearest DataContext.
```xaml
<TextBlock Text="{Binding}" DataContext="James"/>
```
The value bound to `Text="{Binding}"` is passed directly from the nearest DataContext, TextBlock. Therefore, the Binding result value of Text is 'James'.      

#### Type integer
When assigning a value to DataContext directly from Xaml, resource definitions are required first for value types such as Integrer and Boolean because all strings are recognized as String.   
##### First, using System `mscrolib` in Xaml
```xaml
xmlns:sys="clr-namespace:System;assembly=mscorlib"
```
```xaml
<Window.Resources>
    <sys:Int32 x:Key="YEAR">2020</sys:Int32>
</Window.Resources>
...
<TextBlock Text="{Binding}" DataContext="{StaticResource YEAR"/>
```
#### All type of value
```xaml
<Window.Resources>
    <sys:Boolean x:Key="IsEnabled">true</sys:Boolean>
    <sys:double x:Key="Price">7.77</sys:double>
</Window.Resources>
...
<StackPanel>
    <TextBlock Text="{Binding}" DataContext="{StaticResource IsEnabled}"/>
    <TextBlock Text="{Binding}" DataContext="{StaticResource Price}"/>
</StackPanel>
```
> However, there are very few cases where Value Type is binding directly into DataContext.   
Why? Because we're going to bind an object.

### And, Another type
Not only String but also various types are possible. Because DataContext is an object type.


## Binding

### DataContext Binding
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
