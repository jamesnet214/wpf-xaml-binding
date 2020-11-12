# wpfxamlbinding

## Binding
1. Binding
2. MultiBinding
### Binding
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
### MultiTrigger
```xaml
<MultiTrigger>
    <MultiTrigger.Conditions>
        <Condition Property="IsFocused" Value="False"/>
        <Condition Property="Text" Value=""/>
    </MultiTrigger.Conditions>
    <Setter TargetName="PART_ClearButton" Property="Visibility" Value="Collapsed"/>
    <Setter TargetName="PART_PlaceHolder" Property="Visibility" Value="Visible"/>
</MultiTrigger>
```
### Self Property Binding
```xaml
<TextBlock Text="{Binding RelativeSource={RelativeSource Self}, Path=Text}"/>
```
