public class Solution {
   public class MathNode
        {
            public char CharValue { get; set; }
            public MathNode? Left { get; set; }
            public MathNode? Right { get; set; }
        }

        public class DigitMathNode : MathNode
        {
            public int IntValue { get; set; }
        }


        private int? IdentifyRootNodeIndex(List<dynamic> tokenList)
        {
            // find the priority operator
            var divideIndex = tokenList.IndexOf('+');
            if(divideIndex < 0)
            {
                var multIndex = tokenList.IndexOf('-');
                if (multIndex < 0)
                {
                    var addIndex = tokenList.IndexOf('/');
                    if (addIndex < 0)
                    {
                        var subIndex = tokenList.IndexOf('*');
                        if(subIndex < 0)
                        {
                            return null;
                        }
                        return subIndex;
                    }
                    return addIndex;
                }
                return multIndex;
            }
            return divideIndex;

        }


        // return the root of the tree
        public MathNode CreateMathTree(List<dynamic> tokenList)
        {
            if (tokenList.Count == 1)
                return new DigitMathNode { IntValue = (tokenList[0])};

            // look for / and *
            MathNode root = new MathNode();
            var rootNodeIndex = IdentifyRootNodeIndex(tokenList);
            if (rootNodeIndex.HasValue)
            {
                root.CharValue = tokenList[rootNodeIndex.Value];
                // on coupe en deux
                var leftTokenList = tokenList.GetRange(0, rootNodeIndex.Value); 
                var rightTokenList = tokenList.GetRange(rootNodeIndex.Value + 1, tokenList.Count - 1 - rootNodeIndex.Value);

                root.Left = CreateMathTree(leftTokenList);
                root.Right = CreateMathTree(rightTokenList);
            }
            else
            {
                throw new Exception("On devrait pas arriver ici");
            }

            return root;
        }

        public List<dynamic> Tokenize(string s)
        {
            var res = new List<dynamic>();
            var symbols = new[] { '/', '*', '+', '-' };

            var list = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                bool shouldPopQueue = i == s.Length - 1;

                var current = s[i];
                if (symbols.Contains(current)) 
                {
                    shouldPopQueue = true;
                }

                if (char.IsDigit(current)) 
                {
                    list.Add(current);
                }

                if (shouldPopQueue)
                {
                    var numberStringBuilder = new StringBuilder();
                    for (int j = 0; j < list.Count; j++)
                    {
                        numberStringBuilder.Append(list.ElementAt(j));
                    }
                    list.Clear();
                    res.Add(int.Parse(numberStringBuilder.ToString()));
                }

                if (symbols.Contains(current))
                {
                    res.Add(current);
                }

            }
            return res;
        }

        public int Calculate(string s)
        {
            string sWoWhiteSpace = new string(s.Where(c => c != ' ').ToArray());
            var tokenList = Tokenize(sWoWhiteSpace);
            
            if (sWoWhiteSpace == null)
                return 0;

            var mathTree = CreateMathTree(tokenList);
            return EvaluateExpressionTree(mathTree);
        }

        public int EvaluateExpressionTree(MathNode root)
        {
            if(root is DigitMathNode droot)
            {
                return droot.IntValue;
            } 
            else
            {
                switch (root.CharValue)
                {
                    case '/':
                        return EvaluateExpressionTree(root.Left) / EvaluateExpressionTree(root.Right);
                    case '*':
                        return EvaluateExpressionTree(root.Left) * EvaluateExpressionTree(root.Right);
                    case '-':
                        return EvaluateExpressionTree(root.Left) - EvaluateExpressionTree(root.Right);
                    case '+':
                        return EvaluateExpressionTree(root.Left) + EvaluateExpressionTree(root.Right);
                    default:
                        throw new InvalidOperationException("Bad operator");
                }
            }
        }
}