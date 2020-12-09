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
And, all ui controls in wpf inherit the FrameworkElement class.   
> At this point in learning Binding or DataContext, you don't have to study FrameworkElement in greater depth. However, this is to briefly mention the fact that the closest object that can encompass all UI controls is the FrameworkElement.   

### DataContext is always the reference point for Binding.
Binding can directly recall values for the DataContext type format starting with the nearest DataContext.
```xaml
<TextBlock Text="{Binding}" DataContext="James"/>
```
The value bound to `Text="{Binding}"` is passed directly from the nearest DataContext, TextBlock. Therefore, the Binding result value of Text is 'James'.      

#### Type integer
When assigning a value to DataContext directly from Xaml, resource definitions are required first for value types such as Integer and Boolean. Because all strings are recognized as String.   
#### Step 1, using System `mscrolib` in Xaml
Because, simple type variable type is not supported by standard.
```xaml
xmlns:sys="clr-namespace:System;assembly=mscorlib"
```
> You can define it with any word, but mostly use `sys` words.
#### Step 2, create `YEAR` resource key in xaml
Declare the value of the type you want to create in the form of a StaticResource.
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
Because we're going to bind an object.

### And, another type
Not only String but also various types are possible. Because DataContext is an object type.

### Finally
#### DataContext, ****
> In using Binding at WPF, most developers are not fully aware of the existence, function and importance of DataContext. It may mean that Binding is being connected by luck.   

> Especially if you are responsible for or participating in a large WPF project, you should understand the DataContext hierarchy of the application more clearly. In addition, the introduction of WPF's various popular MVVM Framework systems without this DataContext concept will create even greater limitations in implementing functions freely.

## Binding

- ### DataContext Binding
  string property
  ```xaml
  <TextBox Text="{Binding Keywords}"/>
  ```

- ### Binding Element
  ```xaml
  <CheckBox x:Name="ckUseEmail"/>
  <TextBlock Text="{Binding ElementName=ckUseEmail, Path=IsChecked}"/>
  ```
- ### MultiBinding
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
  
  - ### Self Property Binding
  ```xaml
  <TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Tag}"/>
  ```
  
  Truly, same with this code.
  ```xaml
  <TextBlock x:Name="txt" Text="{Binding ElementName=txt, Path=Tag}"/>
  ```
  Yes. You no longer have to declare `x:Name` to bind your own property.
- ### Binding (Find Parent)
  Imports based on the parent control closest to it.
  ```xaml
  <TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}"/>
  ```
  In addition to the properties of the controls found, the properties within the DataContext object can be used if it exists.
  ```xaml
  <TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.Email}"/>
  ```

- ### TemplatedParent
  This is a method that can be used within ControlTemplate, and you can import the control that is the owner of the ControlTemplate.
  ```xaml
  <Style TargetType="Button">
      <Setter Property="Template">
          <Setter.Value>
              <ControlTemplate TargetType="Button">
                  <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent}}"/>
              </ControlTemplate>
          </Setter.Value>
      </Setter>
  ```
  You can access to all Property and DataCotntext.
  ```xaml
  <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Content}"/>
  ```

- ### Static Property Binding
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
      <cvt:VisibilityToBooleanConverter x:Key="VisibilityToBooleanConverter"/>
      <exam:ExamClass x:Key="ExamClass">
  </Window.Resource>
  ...

  <TextBlock Text="{Binding Source={StaticResource ExamClass}, Path=ExamText}"/>
  ```
  > I have never used the Static Property under normal circumstances. This is because data that deviates from its own DataContext can disrupt the flow of whole WPF applications and impair readability significantly. However, this method is actively used in the development stage to implement fast testing and functions, as well as in the DataContext (or ViewModel).
