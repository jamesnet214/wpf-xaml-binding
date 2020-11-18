# wpfxamlbinding
> ncoresoftgit [here.](https://github.com/ncoresoftsource/ncoresoftgit)   
I hope you will also refer to this article for better understanding. [here.](https://github.com/ncoresoftsource/trigger)
## Binding
- Binding
- Binding Element
- MultiBinding
- Self Property Binding
### Binding

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
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Text}"/>
```

### Binding (Find Parent)
Imports based on the parent control closest to it.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=Title}"/>
```
In addition to the properties of the controls found, the properties within the DataContext object can be used if it exists.
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource AncestorType=Window}, Path=DataContext.SomeProperty}"/>
```
