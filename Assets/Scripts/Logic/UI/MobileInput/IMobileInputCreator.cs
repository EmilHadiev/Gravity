using System;

public interface IMobileInputCreator
{
    IMobileInput MobileInput { get; }

    event Action<IMobileInput> InputCreated;
}