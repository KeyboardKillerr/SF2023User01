using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Other;

internal class Counter
{
    public int MaximumValue { get; private init; }
    public int MinimumValue { get; private init; }
    public int CurrentValue { get; private set; }
    public int Step { get; private init; }
    public Counter(int minimumValue, int maximumValue, int step)
    {
        if (minimumValue > maximumValue)
            throw new ArgumentException(
                "Максимальное значение не может быть меньше минимального (меньше 0 при инициализации без указания минимального значения)");
        if (step < 1)
            throw new ArgumentException(
                "Шаг не может быть меньше еденицы");
        MinimumValue = minimumValue;
        MaximumValue = maximumValue;
        CurrentValue = MinimumValue;
        Step = step;
    }
    public Counter(int maximumValue, int step) : this(0, maximumValue, step) { }
    public Counter(int maximumValue) : this(maximumValue, 1) { }
    public void Increase() => CurrentValue = CurrentValue >= MaximumValue ? MaximumValue : CurrentValue + Step;
    public void Decrease() => CurrentValue = CurrentValue <= MinimumValue ? MinimumValue : CurrentValue - Step;
    public void Reset() => CurrentValue = MinimumValue;
    public void SetCurrentValue(int value)
    {
        if (value % Step != 0) value /= Step;

        if (value < MinimumValue) CurrentValue = MinimumValue;
        else if (value > MaximumValue) CurrentValue = MaximumValue;
        else CurrentValue = value;
    }
}
