using System;

/// <summary>
/// ø…∞Û∂® Ù–‘
/// </summary>
/// <typeparam name="T"></typeparam>
public class BindableProperty<T> where T: IEquatable<T>
{
    T _Value=default(T);
    public T Value 
    { 
        get 
        { 
            return _Value; 
        } 
        set 
        {
            if (!value.Equals(_Value))
            {
                _Value = value;
                OnValueChanged?.Invoke(_Value);
            }
            
        }
       
    }
    public Action<T> OnValueChanged;
}
