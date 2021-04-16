# wpf-xaml-binding
### About us

> &nbsp; :adult: __James Lee__ &nbsp;&nbsp; [Github](https://github.com/devncore-james) &nbsp;&nbsp; james.lee@devncore.org  
> &nbsp; :woman: __Elena Kim__ &nbsp;&nbsp; [Github](https://github.com/devncore-elena) &nbsp;&nbsp; elena.kim@devncore.org

We are very ordinary developers, so we need to communicate with you.   
You can always share information with us and we are looking forward to it.  

##### _Open Source &nbsp; https://github.com/devncore/devncore   &nbsp;&nbsp;   Official Website &nbsp; https://devncore.org_ 

### License Policy
[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![GPLv3 license](https://img.shields.io/badge/License-GPLv3-blue.svg)](http://perso.crans.org/besson/LICENSE.html)

***
## Overview
- [DataContext](#datacontext)
- [Binding](#binding)
- [Bad Binding & Good Binding](#bad-binding--good-binding)

<br />

## DataContext
__DataContext is the DependencyProperty included in the FrameworkElement.__  
`PresentationFramework.dll`

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

And, all UI Controls in WPF inherit the `FrameworkElement` class.   
> At this point in learning Binding or DataContext, you don't have to study FrameworkElement in greater depth.  
> However, this is to briefly mention the fact that the closest object that can encompass all UI Controls is the FrameworkElement.   
<br />

### _DataContext is always the reference point for Binding._ 
Binding can directly recall values for the DataContext type format starting with the nearest DataContext.
```xaml
<TextBlock Text="{Binding}" DataContext="James"/>
```
The value bound to `Text="{Binding}"` is passed directly from the nearest DataContext, `TextBlock`.  
Therefore, the Binding result value of `Text` is 'James'.      
<br />

- __Type integer__  
When assigning a value to DataContext directly from Xaml, resource definitions are required first for value types such as Integer and Boolean.
Because all strings are recognized as String.   

    #### 1. Using System `mscrolib` in Xaml
    > Simple type variable type is not supported by standard.  
    > You can define it with any word, but mostly use `sys` words.  
    ```xaml
    xmlns:sys="clr-namespace:System;assembly=mscorlib"
    ```

    #### 2. Create `YEAR` resource key in xaml
    > Declare the value of the type you want to create in the form of a StaticResource.
    ```xaml
    <Window.Resources>
        <sys:Int32 x:Key="YEAR">2020</sys:Int32>
    </Window.Resources>
    ...
    <TextBlock Text="{Binding}" DataContext="{StaticResource YEAR"/>
    ```

- __All type of value__  
There are very few cases where Value Type is binding directly into DataContext.   
Because we're going to bind an object.
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

- __Another type__  
Not only String but also various types are possible. Because DataContext is an object type.
<br />

### Finally... 
In using Binding at WPF, most developers are not fully aware of the existence, function and importance of DataContext.  
It may mean that Binding is being connected by luck.   
> __Especially if you are responsible for or participating in a large WPF project, you should understand the DataContext hierarchy of the application more clearly. In addition, the introduction of WPF's various popular MVVM Framework systems without this DataContext concept will create even greater limitations in implementing functions freely.__
<br />

* * *  
## Binding
- [DataContext Binding](#datacontext-binding)
- [Element Binding](#element-binding)
- [MultiBinding](#multibinding)
- [Self Property Binding](#self-property-binding)
- [Find Ancestor Binding](#find-ancestor-binding)
- [TemplatedParent Binding](#templatedparent-binding)
- [Static Property Binding](#static-property-binding)  
<br />

### DataContext Binding
`string property`
```xaml
<TextBox Text="{Binding Keywords}"/>
```
<br />

### Element Binding
```xaml
<CheckBox x:Name="usingEmail"/>
<TextBlock Text="{Binding ElementName=usingEmail, Path=IsChecked}"/>
```
<br />

### MultiBinding
```xaml
<TextBlock Margin="5,2" Text="This disappears as the control gets focus...">
  <TextBlock.Visibility>
      <MultiBinding Converter="{StaticResource TextInputToVisibilityConverter}">
          <Binding ElementName="txtUserEntry2" Path="Text.IsEmpty" />
          <Binding ElementName="txtUserEntry2" Path="IsFocused" />
      </MultiBinding>
  </TextBlock.Visibility>
</TextBlock>
```
<br />
 
### Self Property Binding

```xaml
<TextBlock x:Name="txt" Text="{Binding ElementName=txt, Path=Tag}"/>
```
If you have to bind your own property, you can use `Self Property Binding`, instead of using `Element Binding`.  
You no longer have to declare `x:Name` to bind your own property.  

```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Tag}"/>
```
<br />
 
### Find Ancestor Binding
Imports based on the parent control closest to it.

```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}"/>
```

In addition to the properties of the controls found, the properties within the DataContext object can be used if it exists.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.Email}"/>
```
<br />

If you want to bind another `DataContext` other than the currently bound `DataContext`, you can use the following method.
```csharp
public partial class A : UserControl
{
    public A()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
    
public class MainViewModel
{
    public B G1VM { get; set; } = new B();
    public C G2VM { get; set; } = new C();
}
```
```xaml
<TabControl DataContext="{Binding G1VM}">
    <TabItem Header="TMP">
        <DataGrid ItemsSource="{Binding datagrid}" 
                  DataContext="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.G1VM}"/>
    </TabItem>
</TabControl>
```
```xaml
<TabControl DataContext="{Binding G1VM}">
    <TabItem Header="TMP">
        <DataGrid ItemsSource="{Binding datagrid}" 
                  DataContext="{Binding Parent.G2VM}"/>
    </TabItem>
</TabControl>
```

### TemplatedParent Binding
This is a method that can be used within `ControlTemplate`, and you can import the control that is the owner of the `ControlTemplate`.
```xaml
<Style TargetType="Button">
  <Setter Property="Template">
      <Setter.Value>
          <ControlTemplate TargetType="Button">
              <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Content}"/>
          </ControlTemplate>
      </Setter.Value>
  </Setter>
```

You can access to all Property and DataContext.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Content}"/>
```
<br />

### Static Property Binding
You can access binding property value directly.   

#### 1. Declare `static` property.
```csharp
namespace Exam
{
  public class ExamClass
  {
      public static string ExamText { get; set; }
  }
} 
```

#### 2. Using static class in XAML.
```xaml
<Window ... xmlns:exam="clr-namespace:Exam">
```

#### 3. Binding property.
```xaml
<TextBlock Text="{Binding exam:ExamClass.ExamText}"/>
```

_Or, you can set Resource key like using `Converter`._  
```xaml
<Window.Resource>
  <cvt:VisibilityToBooleanConverter x:Key="VisibilityToBooleanConverter"/>
  <exam:ExamClass x:Key="ExamClass">
</Window.Resource>
...

<TextBlock Text="{Binding Source={StaticResource ExamClass}, Path=ExamText}"/>
```

> I have never used the Static Property under normal circumstances. This is because data that deviates from its own DataContext can disrupt the flow of whole WPF applications and impair readability significantly. However, this method is actively used in the development stage to implement fast testing and functions, as well as in the DataContext (or ViewModel).  
<br />

* * *  
## Bad Binding & Good Binding 

### :heavy_check_mark: If the property you want to bind is included in Datacontext, <br /> &nbsp; &nbsp; &nbsp; you don't have to use ElementBinding.
&nbsp; &nbsp; &nbsp; _Using ElementBinding through connected control is not a functional problem,  
&nbsp; &nbsp; &nbsp; <ins>but it breaks the fundamental pattern of Binding</ins>._   

#### :slightly_frowning_face: Bad Binding 
```xaml
<TextBox x:Name="text" Text="{Binding UserName}"/>
...
<TextBlock Text="{Binding ElementName=text, Path=Text}"/>
```
   
#### :grinning: Good Binding
```xaml
<TextBox Text="{Binding UserName}"/>
...
<TextBlock Text="{Binding UserName}"/>
```
<br />

### :heavy_check_mark: Do not use ElementBinding when using property belonging to higher layers control.   

#### :slightly_frowning_face: Bad Binding 
```xaml
<Window x:Name="win">
  <TextBlock Text="{Binding ElementName=win, Path=DataContext.UserName}"/>
  ...
```
    
#### :grinning: Good Binding
```xaml
<Window>
  <TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.UserName}"/>
  ...
```      

#### :laughing: Great!
```xaml
<Window>
  <TextBlock DataContext="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext}" 
             Text="{Binding UserName}"/>
  ...
```
<br />

### :heavy_check_mark: Do not use ElementBinding when using your own properties.   

#### :slightly_frowning_face: Bad Binding 
```xaml
<TextBlock x:Name="txt" Text="{Binding ElementName=txt, Path=Foreground}"/>
```

#### :grinning: Good Binding
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Foreground}"/>
```
