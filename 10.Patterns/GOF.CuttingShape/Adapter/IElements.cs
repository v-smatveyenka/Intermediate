﻿namespace Adapter
{
    public interface IElements<T>
    {
        IEnumerable<T> GetElements();
    }
}
