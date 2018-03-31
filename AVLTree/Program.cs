using System;
using System.Collections.Generic;

namespace AVLTree
{
    class Program
    {
        static void Main(string[] args)
        {




            Random r = new Random();
           
            AVLTree tree = new AVLTree() { };
            //for (int i = 0; i < 31; i++)
            //{
            //    int el = r.Next(100);
            //    // Console.WriteLine();
            //    PrintTree(tree.Root);
            //    Console.WriteLine();
            //   Node toAdd = tree.Add(el);
            //    if (toAdd != null)
            //    {
            //        Node defectNode = AVLTree.FindMinTreeWIthDefect(toAdd);
            //        if (defectNode != null)
            //        {
            //            PrintTree(tree.Root);

            //            Console.WriteLine(el);
            //            Console.ReadLine();

            //        }
            //        Console.Clear();
            //    }
                


            //}

            tree.Add(5);
            tree.Add(2);
            tree.Add(51);
            tree.Add(1);
            tree.Add(41);
            tree.Add(75);
            tree.Add(30);
            tree.Add(67);
            tree.Add(82);

            PrintTree(tree.Root);
            Console.WriteLine();

            Node x = tree.Add(28);
            PrintTree(tree.Root);
            Node def = AVLTree.FindMinTreeWIthDefect(x);
            //Console.WriteLine();
            //PrintTree(tree.Root);
            //Console.ReadLine();
            //Console.Clear();

        }

        public static void PrintTree(Node root)
        {
            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(root);

            int level = 0;

            while (true)
            {
                int counter = 0;
                string s = "";
                for (int i = 0; i < Math.Pow(2, level); i++)
                {

                    var el = queue.Dequeue();
                    if (el == null)
                    {
                        s += "n ";
                        queue.Enqueue(null);
                        queue.Enqueue(null);
                    }
                    else
                    {
                        counter++;
                        s += (el.Data + " ");
                        queue.Enqueue(el.Left);
                        queue.Enqueue(el.Right);
                    }
                    if (s.Length > 0)
                    {

                    }
                }
                level++;
                if (counter == 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(s);
                }
            }

        }
    }
}
