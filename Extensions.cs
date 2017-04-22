using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning.Structures;
using Accord.Math;
using Assets.Scripts.PathFinding;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts.Core {

    public static class Extensions {
        public static void ForEach<T>( this IEnumerable<T> from, Action<T> callback ) {
            foreach ( var element in from ) {
                callback( element );
            }
        }

        public static double[] ToArray( this Vector3 vec ) {
            return new double[] { vec.x, vec.y, vec.z };
        }

		public static Vector3 Position( this Node node ) {
			return node.InformerNode.transform.position;
		}

        public static float MetricsAStar( this Informer from, Informer to ) {
            var metric = ( to.transform.position - from.transform.position ).sqrMagnitude;
            return metric;
        }

        public static float Metrics(Tree_Node from, Node to)
        {
            var metricAStar = MetricsAStar(from.Currentnode.InformerNode, to.InformerNode);
            if (from.Parent != null)
            {
                var metric = (to.Position - from.Currentnode.Position).sqrMagnitude +
                             (from.Currentnode.Position - from.Parent.Position).sqrMagnitude;
                return metric;
            }
            else return metricAStar;
        }

        public static List<Node> ToList( this KDTreeNodeList<Informer> tree ) {
            return tree
                .Select( node => new Node( node.Node.Value, NodeState.Undiscovered ) )
                .ToList();
        }

        public static List<Node> ToNodes(List<Point> points, Node [,] nodesArray)
        {
            var list = new List<Node>();
            for (var i=0;i<points.Count;++i)
            {
                list.Add(nodesArray[points[i].X,points[i].Y]);
            }
            return list;
        }

        public static List<Node> ToNodes(List<Tree_Node> nodes)
        {
            var list = new List<Node>();
            for (var i = 0; i < nodes.Count; ++i)
            {
                list.Add(nodes[i].Currentnode);
            }
            return list;
        }

        public static List<Informer> ToInformers(List<Node> nodes)
        {
            var list = new List<Informer>();
            for (var i = 0; i < nodes.Count; ++i)
            {
                list.Add(nodes[i].InformerNode);
            }
            return list;
        }
    }
}