# wpfxamlbinding

## Binding
1. Binding
2. MultiBinding
### Binding
### MultiBinding
```
<TextBlock Margin="5,2" Text="This dissappears as the control gets focus..." Foreground="{StaticResource brushWatermarkForeground}" >
    <TextBlock.Visibility>
        <MultiBinding Converter="{StaticResource TextInputToVisibilityConverter}">
            <Binding ElementName="txtUserEntry2" Path="Text.IsEmpty" />
            <Binding ElementName="txtUserEntry2" Path="IsFocused" />
        </MultiBinding>
    </TextBlock.Visibility>
</TextBlock>
```
