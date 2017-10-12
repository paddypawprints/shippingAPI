/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi
{
    public class RequestFinderVisitor<RequestType, ReportType> : ExpressionVisitor where RequestType : new()
    {
        public Expression Expression { get; set; }
        protected RequestType request;

        public RequestFinderVisitor()
        {
        }

        public RequestType Request
        {
            get
            {
                if (request == null)
                {
                    request = new RequestType();
                    this.Visit(Expression);
                }
                return this.request;
            }
        }

        protected Expression AssignExpressionValue<T>(BinaryExpression be, ExpressionType expressionType, string propertyName, Action<RequestType, T> assign)
        {
            if (be.NodeType == expressionType && IsMemberValueExpression(be, expressionType, propertyName))
            {
                assign(request, GetValueFromBinaryExpression<T>(be, expressionType, propertyName));
                return be;
            }
            return null;
        }

        protected override Expression VisitBinary(BinaryExpression be)
        {
            return base.VisitBinary(be);
        }

        internal T GetValueFromBinaryExpression<T>(BinaryExpression be, ExpressionType expressionType,  string memberName)
        {
            if (be.NodeType != expressionType)
                throw new Exception("There is a bug in this program.");

            var memberDeclaringType = typeof(ReportType);

            if (be.Left.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression me = (MemberExpression)be.Left;

                if (memberDeclaringType.IsAssignableFrom(me.Member.DeclaringType) && me.Member.Name == memberName)
                {
                    return GetValueFromExpression<T>(be.Right);
                }
            }
            else if (be.Right.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression me = (MemberExpression)be.Right;

                if (memberDeclaringType.IsAssignableFrom(me.Member.DeclaringType) && me.Member.Name == memberName)
                {
                    return GetValueFromExpression<T>(be.Left);
                }
            }

            // We should have returned by now. 
            throw new InvalidProgramException("GetValueFromBinaryExpression: There is a bug in this program - expression is messed up.");
        }

        internal T GetValueFromExpression<T>(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
                return (T)(((ConstantExpression)expression).Value);
            else
                throw new ArgumentException(
                    String.Format("GetValueFromExpression: The expression type {0} is not supported to obtain a value.", expression.NodeType)); 
        }
        internal bool IsMemberValueExpression(Expression exp, ExpressionType expressionType, string memberName)
        {
            if (exp.NodeType != expressionType)
                return false;

            BinaryExpression be = (BinaryExpression)exp;
            var declaringType = typeof(ReportType);
            // Assert. 
            if (IsSpecificMemberExpression(be.Left, declaringType, memberName) &&
                IsSpecificMemberExpression(be.Right, declaringType, memberName))
                throw new ArgumentException("IsMemberValueExpression: Cannot have 'member' == 'member' in an expression!");

            return (IsSpecificMemberExpression(be.Left, declaringType, memberName) ||
                IsSpecificMemberExpression(be.Right, declaringType, memberName));
        }

        internal bool IsSpecificMemberExpression(Expression exp, Type declaringType, string memberName)
        {
            return ((exp is MemberExpression) &&
                (declaringType.IsAssignableFrom(((MemberExpression)exp).Member.DeclaringType)) &&
                (((MemberExpression)exp).Member.Name == memberName));
        }
    }


}

