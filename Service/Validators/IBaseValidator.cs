using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Validators
{
    public interface IBaseValidator<T>
    {
        List<ValidationResult> Validate(T obj);
    }
}
