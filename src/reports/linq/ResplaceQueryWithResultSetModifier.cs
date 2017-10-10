/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PitneyBowes.Developer.ShippingApi
{
    internal class ReplaceQueryWithResultSetModifier<TResult, TReport> : ExpressionVisitor
    {
        private TReport _queryableTransactions;

        public ReplaceQueryWithResultSetModifier(TReport transactions)
        {
            _queryableTransactions = transactions;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (IsAssignableGeneric(typeof(TReport), c.Type))
                return Expression.Constant(_queryableTransactions);
            else
                return c;
        }

        protected bool IsAssignableGeneric(Type type, Type typeFrom)
        {
            return type.IsAssignableFrom(typeFrom);
        }

    }

 }

