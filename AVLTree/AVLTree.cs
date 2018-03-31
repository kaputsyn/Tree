using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{
    public class AVLTree
    {
        public Node Root;

        private static int FindHeight(Node rootHeight)
        {
            if (rootHeight == null)
            {
                return 0;
            }
            return Math.Max(FindHeight(rootHeight.Left), FindHeight(rootHeight.Right)) + 1;
        }
        private static Node RotateRight(Node toRotate)
        {
            Node r2 = toRotate.Left.Right;
            Node l2 = toRotate.Left.Left;

            Node r1 = toRotate.Right;
            Node l = toRotate.Left;


            if (toRotate.Parent != null && (toRotate.Parent.Left == toRotate))
            {
                l.Parent = toRotate.Parent;
                l.Parent.Left = l;

                l.Left = l2;
                l.Right = toRotate;

                toRotate.Parent = l;

                toRotate.Left = r2;
            }
            else if (toRotate.Parent != null && (toRotate.Parent.Right == toRotate))
            {
                l.Parent = toRotate.Parent;
                l.Parent.Right = l;

                l.Left = l2;
                l.Right = toRotate;

                toRotate.Parent = l;

                toRotate.Left = r2;
            }
            else if(toRotate.Parent == null)
            {
                l.Parent = toRotate.Parent;

                l.Left = l2;
                l.Right = toRotate;

                toRotate.Parent = l;

                toRotate.Left = r2;
            }
            return l;


        }
        private static Node RotateLeft(Node toRotate)
        {
            Node l1 = toRotate.Left;
            Node r = toRotate.Right;

            Node l2 = r.Left;
            Node r2 = r.Right;

            if (toRotate.Parent != null && (toRotate.Parent.Left == toRotate))
            {
                r.Parent = toRotate.Parent;

                r.Parent.Left = r;

                r.Left = toRotate;
                r.Right = r2;

                toRotate.Parent = r;
                toRotate.Right = l2;
            }
            else if (toRotate.Parent != null && (toRotate.Parent.Right == toRotate))
            {
                r.Parent = toRotate.Parent;

                r.Parent.Right = r;

                r.Left = toRotate;
                r.Right = r2;

                toRotate.Parent = r;
                toRotate.Right = l2;
            }
            else if (toRotate.Parent == null)
            {
                r.Parent = toRotate.Parent;
               
                r.Left = toRotate;
                r.Right = r2;

                toRotate.Parent = r;
                toRotate.Right = l2;
            }

            return r;
        }

        public static Node FindMinTreeWIthDefect(Node addedElement)
        {
            Node parrent = addedElement.Parent;
            if (parrent == null) { return null; }
            if (GetDefect(parrent) > 1 || GetDefect(parrent) < -1)
            {
                return parrent;
            }
            return FindMinTreeWIthDefect(parrent);
        }

        private static int GetDefect(Node defectRoot)
        {
            if (defectRoot == null) { return 0; }
            if (defectRoot.Left == null && defectRoot.Right == null)
            {
                return 0;
            }
            if (defectRoot.Left == null)
            {
                return 0 - defectRoot.Right.Height;
            }
            if (defectRoot.Right == null)
            {
                return defectRoot.Left.Height;
            }

            return defectRoot.Left.Height - defectRoot.Right.Height;
        }

        private static void SetHeights(Node rootHeight)
        {
            if (rootHeight == null)
            {
                return;
            }
            rootHeight.Height = FindHeight(rootHeight);

            SetHeights(rootHeight.Left);
            SetHeights(rootHeight.Right);

        }

        private static Node FindPosition(Node rootPosition, int value)
        {
            if (rootPosition.Data > value && rootPosition.Left == null)
            {
                return rootPosition;   
            }
            if (rootPosition.Data > value && rootPosition.Left != null)
            {
                return FindPosition(rootPosition.Left, value);
            }

            if (rootPosition.Data < value && rootPosition.Right == null)
            {
                return rootPosition;
            }
            if (rootPosition.Data < value && rootPosition.Right != null)
            {
                return FindPosition(rootPosition.Right, value);
            }

            return null;
        }
        public Node Add(int element)
        {
            if (element == -5)
            {
            }
            if (this.Root == null)
            {
                this.Root = new Node() { Data = element, Height = 1 };
                SetHeights(this.Root);
                return this.Root;
            }
            Node positionNode = FindPosition(this.Root,element);

            if (positionNode == null)
            { return null; }

            Node toAdd = new Node() { Data = element };
            if (positionNode.Data > element)
            {
                positionNode.Left = toAdd;
                
            }
            else
            {
                positionNode.Right = toAdd;
            }
            toAdd.Parent = positionNode;

            SetHeights(this.Root);
            Node defectRoot = FindMinTreeWIthDefect(toAdd);
            if (defectRoot != null)
            {
                int defect = GetDefect(defectRoot);
                if (defect == 2)
                {
                    if (GetDefect(defectRoot.Left) < 0)
                    {
                        Node nodeRoot = RotateLeft(defectRoot.Left);
                        SetHeights(this.Root);
                    }
                    Node bigRoot = RotateRight(defectRoot);
                    if (bigRoot.Parent == null)
                    {
                        this.Root = bigRoot;
                    }
                    SetHeights(this.Root);
                }
                if (defect == -2)
                {
                    if (GetDefect(defectRoot.Right) > 0)
                    {
                        Node nodeRoot = RotateRight(defectRoot.Right);
                        SetHeights(this.Root);
                    }
                    Node bigRoot = RotateLeft(defectRoot);
                    if (bigRoot.Parent == null)
                    {
                        this.Root = bigRoot;
                    }
                    SetHeights(this.Root);
                }
            }
            return toAdd;
        }
    }
}
