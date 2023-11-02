using System;
using System.Linq;
using System.Reflection;

namespace Documentation;

public class Specifier<T> : ISpecifier
{
    Type type = typeof(T);
    public string GetApiDescription()
	{
        var attribute = type.GetCustomAttributes(true).OfType<ApiDescriptionAttribute>().FirstOrDefault();

        if (attribute == null)
            return null;

        return attribute.Description;
    }

	public string[] GetApiMethodNames()
	{
        return type.GetMethods()
            .Where(method => method.IsPublic && method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            .Select(method => method.Name)
            .ToArray();
    }

	public string GetApiMethodDescription(string methodName)
	{
		var method = type.GetMethod(methodName);

		if (method == null || !method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
			return null;
		
        var attribute = method.GetCustomAttributes(true).OfType<ApiDescriptionAttribute>().FirstOrDefault();
        
        if (attribute == null)
            return null;
        
        return attribute.Description;
    }

	public string[] GetApiMethodParamNames(string methodName)
	{
        var method = type.GetMethod(methodName);
        
        if (method == null || !method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            return null;
        
        return method.GetParameters()
						.Select(param => param.Name)
						.ToArray();
    }

	public string GetApiMethodParamDescription(string methodName, string paramName)
	{
        var method = type.GetMethod(methodName);
        
        if (method == null || !method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            return null;
        
        var parameter = method.GetParameters()
                                .Where(param => param.Name == paramName);
        
        if (!parameter.Any())
            return null;
        
        var attribute = parameter.First().GetCustomAttributes(true).OfType<ApiDescriptionAttribute>().FirstOrDefault();
        
        if (attribute == null)
            return null;
        
        return attribute.Description;
    }

	public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
	{
        var result = new ApiParamDescription();
        result.ParamDescription = new CommonDescription(paramName);
        var method = type.GetMethod(methodName);
        
        if (method != null && method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
        {
            var parameter = method.GetParameters()
                                    .Where(param => param.Name == paramName);
            if (parameter.Any())
            {
                var Attribute = parameter.First().GetCustomAttributes(true).OfType<ApiDescriptionAttribute>().FirstOrDefault();
                
                if (Attribute != null)
                    result.ParamDescription.Description = Attribute.Description;
                
                var intValidationAttribute = parameter.First().GetCustomAttributes(true).OfType<ApiIntValidationAttribute>().FirstOrDefault();
                
                if (intValidationAttribute != null)
                {
                    result.MinValue = intValidationAttribute.MinValue;
                    result.MaxValue = intValidationAttribute.MaxValue;
                }
                
                var requiredAttribute = parameter.First().GetCustomAttributes(true).OfType<ApiRequiredAttribute>().FirstOrDefault();
                
                if (requiredAttribute != null)
                    result.Required = requiredAttribute.Required;
            }
        }
        return result;
    }

	public ApiMethodDescription GetApiMethodFullDescription(string methodName)
	{
        var result = new ApiMethodDescription();

        var method = type.GetMethod(methodName);

        var returnParameter = method.ReturnParameter;
        var isNecessaryToSetReturnParameter = false;

        var returnParamDiscription = new ApiParamDescription();
        returnParamDiscription.ParamDescription = new CommonDescription();


        
        if (method == null || !method.GetCustomAttributes(true).OfType<ApiMethodAttribute>().Any())
            return null;
        
        result.MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName));
        result.ParamDescriptions = GetApiMethodParamNames(methodName)
                                    .Select(param => GetApiMethodParamFullDescription(methodName, param))
                                    .ToArray();

        var descriptionAttribute = returnParameter.GetCustomAttributes(true).OfType<ApiDescriptionAttribute>().FirstOrDefault();
        
        if (descriptionAttribute != null)
        {
            returnParamDiscription.ParamDescription.Description = descriptionAttribute.Description;
            isNecessaryToSetReturnParameter = true;
        }
        
        var intValidationAttribute = returnParameter.GetCustomAttributes(true).OfType<ApiIntValidationAttribute>().FirstOrDefault();
        
        if (intValidationAttribute != null)
        {
            returnParamDiscription.MinValue = intValidationAttribute.MinValue;
            returnParamDiscription.MaxValue = intValidationAttribute.MaxValue;
            isNecessaryToSetReturnParameter = true;
        }
        
        var requiredAttribute = returnParameter.GetCustomAttributes(true).OfType<ApiRequiredAttribute>().FirstOrDefault();
        
        if (requiredAttribute != null)
        {
            returnParamDiscription.Required = requiredAttribute.Required;
            isNecessaryToSetReturnParameter = true;
        }

        if (isNecessaryToSetReturnParameter)
            result.ReturnDescription = returnParamDiscription;
        
        return result;
    }
}