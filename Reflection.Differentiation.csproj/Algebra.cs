﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.Differentiation
{
    public static class Algebra
    {
        private static Expression BinaryOperation(Expression body)
        {
            var operation = (BinaryExpression)body;
            var left = operation.Left;
            var right = operation.Right;
            if (operation.NodeType == ExpressionType.Multiply) //(fg) = f'g + fg'
                return Expression.Add(Expression.Multiply(DerivativeOperation(left), right),
                Expression.Multiply(DerivativeOperation(right), left));
            if (operation.NodeType == ExpressionType.Add)
                return Expression.Add(DerivativeOperation(left), DerivativeOperation(right));
            throw new ArgumentException($"Unsupported binary operation '{operation.NodeType}'");
        }
        private static Expression MethodCall(Expression body)
        {
            var method = (MethodCallExpression)body;
            var arg = method.Arguments[0];
            var newBody = body;
            if (method.Method.Name == "Cos")
                newBody = Expression.Negate(Expression.Call(typeof(Math).GetMethod("Sin", new[] { typeof(double) }), arg));
            else if (method.Method.Name == "Sin")
                newBody = Expression.Call(typeof(Math).GetMethod("Cos", new[] { typeof(double) }), arg);
            else
                throw new ArgumentException($"Unknown function '{method.Method.Name}'");
            return Expression.Multiply(newBody, (DerivativeOperation(arg)));
        }

        private static Expression DerivativeOperation(Expression body)
        {
            if (body is ConstantExpression) return Expression.Constant(0.0);
            if (body is ParameterExpression) return Expression.Constant(1.0);
            if (body is MethodCallExpression) return MethodCall(body);
            if (body is BinaryExpression) return BinaryOperation(body);
            throw new ArgumentException("String containing \"ToString\"");
        }

        public static Expression<Func<double, double>> Differentiate(Expression<Func<double, double>> expression)
        {
            var body = expression.Body;
            var parameters = expression.Parameters;
            var derivative = DerivativeOperation(body);
            if (derivative == null)
                throw new ArgumentException("Unable to calculate derivative");

            return Expression.Lambda<Func<double, double>>(derivative, parameters);
        }
    }
}