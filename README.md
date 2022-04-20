## Xaml Binding
  
이 리포지토리는 WPF Xaml Binding 개념과 기술을 활용하는데 필요한 설명을 다루는 Article입니다.

<a href="https://github.com/devncore/devncore"><strong>더 알아보기 »</strong></a>
  
| Star | License | Activity |
|:----:|:-------:|:--------:|
| <a href="https://github.com/devncore/wpf-xaml-binding/stargazers"><img src="https://img.shields.io/github/stars/devncore/wpf-xaml-binding" alt="Github Stars"></a> | <img src="https://img.shields.io/github/license/devncore/wpf-xaml-binding" alt="License"> | <a href="https://github.com/devncore/wpf-xaml-binding/pulse"><img src="https://img.shields.io/github/commit-activity/m/devncore/wpf-xaml-binding" alt="Commits-per-month"></a> |

<br />

## Overview
- [DataContext](#datacontext)
- [Binding](#binding)
- [Bad Binding & Good Binding](#bad-binding--good-binding)

<br />

## DataContext
__DataContext는 FrameworkElement에 포함된 속성입니다.__  
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

그리고 WPF의 모든 UI 컨트롤은 `FrameworkElement` 클래스를 상속합니다. 
> 바인딩 또는 데이터 컨텍스트를 배워가는 시점에서 FrameworkElement를 더 깊이 연구할 필요가 없습니다.
> 하지만 모든 UI 컨트롤을 포함할 수 있는 가장 가까운 개체가 FrameworkElement라는 사실을 간단히 언급하기 위함입니다.
<br />

### _DataContext is always the reference point for Binding._ 
Binding can directly recall values for the DataContext type format starting with the nearest DataContext.
```xaml
<TextBlock Text="{Binding}" DataContext="James"/>
```
`Text="{Binding}`에 바인딩된 값은 가장 가까운 데이터 컨텍스트인 `TextBlock`에서 직접 전달됩니다.    
따라서 `Text`의 바인딩 결과 값은 'James'입니다.
<br />

- __Type integer__  
Xaml에서 DataContext에 직접 값을 할당하는 경우 정수 및 부울과 같은 값 유형에 대해 먼저 리소스 정의가 필요합니다.
왜냐하면 모든 문자열이 문자열로 인식되기 때문입니다.

    #### 1. Xaml에서 System `mscrollib` 사용
    > Simple type variable type is not supported by standard.  
    > 어떤 단어로도 정의할 수 있지만 대부분 `sys` 단어를 사용합니다.
    ```xaml
    xmlns:sys="clr-namespace:System;assembly=mscorlib"
    ```

    #### 2. xaml에서 `YEAR` 리소스 키 생성
    > StaticResource 형식으로 생성할 유형의 값을 선언합니다.
    ```xaml
    <Window.Resources>
        <sys:Int32 x:Key="YEAR">2020</sys:Int32>
    </Window.Resources>
    ...
    <TextBlock Text="{Binding}" DataContext="{StaticResource YEAR"/>
    ```

- __All type of value__  
값이 데이터 컨텍스트에 직접 바인딩되는 경우는 거의 없습니다.    
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
스트링뿐만 아니라 다양한 타입이 가능합니다. 데이터 컨텍스트가 객체이기 때문입니다.
<br />

### WPF에서 바인딩을 사용할 때,

 대부분의 개발자들은 DataContext의 존재, 기능 및 중요성에 대해 완전히 알지 못합니다.    
It may mean that Binding is being connected by luck.   
> __Especially if you are responsible for or participating in a large WPF project, you should understand the DataContext hierarchy of the application more clearly. In addition, the introduction of WPF's various popular MVVM Framework systems without this DataContext concept will create even greater limitations in implementing functions freely.__    
> <br />
>  __특히 대규모 WPF 프로젝트를 담당하거나 참여하는 경우 애플리케이션의 DataContext 계층을 보다 명확하게 이해해야 합니다. 또한 이 DataContext 개념이 없으면 기능을 자유롭게 구현하는 데 한계가 있을 것입니다.__
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
자신의 속성을 바인딩해야 하는 경우 요소 바인딩 대신 `Element Binding`을 사용할 수 있습니다.    
자신의 속성을 바인딩하기 위해 `x:Name`을 선언할 필요가 없습니다.

```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Tag}"/>
```
<br />
 
### Find Ancestor Binding

가장 가까운 상위 컨트롤을 기준으로 가져옵니다.

```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}"/>
```
<br />

'Trigger'에서 상위 개체의 속성에 액세스할 때 유용합니다.
```xaml
<DataTrigger Binding="{Binding RelativeSource={RelativeSource AncestorType=ListBoxItem}, Path=IsSelected}" Value="True">
    <Setter Property="Background" Value="#DDDDDD"/>
</DataTrigger>
```
<br />

In addition to the properties of the controls found, the properties within the DataContext object can be used if it exists.  
However, it is recommended that you avoid accessing `DataContext` (ViewModel) of the parent object as much as possible.
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
}
```
```xaml
<TabControl DataContext="{Binding G1VM}">
  <TabItem Header="TMP">
    <DataGrid ItemsSource="{Binding datagrid}" 
              DataContext="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.G1VM}"/>
    <!--It can also be expressed as follows.-->
    <!--<DataGrid ItemsSource="{Binding datagrid}" 
                  DataContext="{Binding Parent.G2VM}"/>-->
  </TabItem>
</TabControl>
```
<br />

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
</Style>
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

## Reference
[:bookmark_tabs:](https://stackoverflow.com/questions/84278/how-do-i-use-wpf-bindings-with-relativesource) **StackOverflow** &nbsp; <ins>How do I use WPF bindings with RelativeSource?</ins>

<br />
