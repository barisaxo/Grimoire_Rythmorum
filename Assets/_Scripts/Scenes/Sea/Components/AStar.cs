using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    public static ITile[] NewSailingPath(
        this ITile targetTile,
        ITile startTile,
        ITile[] tiles,
        int boardSize)
    {
        if (startTile == targetTile) { return new List<ITile>().ToArray(); }

        List<ITile> path = new List<ITile>();
        List<Node> nodes = new List<Node>();
        Node startNode = new Node(startTile, startTile, targetTile);
        Node target = new Node(targetTile, startTile, targetTile);

        if (targetTile.IsOpen) path.Add(targetTile);
        AddAdjacentOpenTiles(startTile, target);

        if (NoOpenNodes()) return new List<ITile>().ToArray();

        Node currentNode = CheapestOpenNode();
        Node prevNode = currentNode;

        while (currentNode.T != target.T)
        {
            CloseNode(currentNode);
            AddAdjacentOpenTiles(currentNode.T, target);
            if (NoOpenNodes()) return new List<ITile>().ToArray();
            prevNode = currentNode;
            currentNode = CheapestOpenNode();
        }

        if (!path.Contains(target.T) && target.T.IsOpen) path.Add(target.T);

        target = prevNode;

        while (target.T != startNode.T)
        {
            nodes.Clear();
            currentNode = startNode;

            while (currentNode.T != target.T)
            {
                CloseNode(currentNode);
                AddAdjacentOpenTiles(currentNode.T, target);
                if (NoOpenNodes()) { return new List<ITile>().ToArray(); }
                prevNode = currentNode;
                currentNode = CheapestOpenNode();
            }

            if (!path.Contains(target.T) && target.T.IsOpen) path.Add(target.T);
            target = prevNode;
        }

        return path.ToArray();

        #region INTERNAL
        void AddAdjacentOpenTiles(ITile tile, Node target)
        {
            CheckTile(Vector2.up);
            CheckTile(Vector2.down);
            CheckTile(Vector2.left);
            CheckTile(Vector2.right);

            void CheckTile(Vector2 v2)
            {
                if (!IsInBounds(tile.Coord + v2)) return;
                ITile t = tiles[(tile.Coord + v2).Vec2ToInt(boardSize)];
                if (!t.IsOpen) return;
                if (AlreadyInNodes(t)) return;
                nodes.Add(new Node(t, startTile, target.T));
            }

            bool AlreadyInNodes(ITile t) { foreach (Node n in nodes) { if (n.T == t) return true; } return false; }
            bool IsInBounds(Vector2 v2) => v2.x >= 0 && v2.x < boardSize && v2.y >= 0 && v2.y < boardSize;
        }

        bool NoOpenNodes()
        {
            if (nodes.Count == 0) return true;
            foreach (Node n in nodes) if (!n.IsClosed) return false;
            return true;
        }

        void CloseNode(Node node) { node.IsClosed = true; }

        Node CheapestOpenNode()
        {
            int lowestF = int.MaxValue;
            Node cheapestNode = nodes[0];

            foreach (Node n in nodes)
            {
                if (n.F <= lowestF && !n.IsClosed)
                {
                    if (n.F < lowestF) { cheapestNode = n; lowestF = n.F; }
                    else if (n.H < cheapestNode.H) { cheapestNode = n; lowestF = n.F; }
                }
            }
            return cheapestNode;
        }

        #endregion
    }



    public static bool IsTileReachable(this ITile targetTile, ITile startTile, ITile[] tiles, int boardSize)
    {
        return NewPath(targetTile, startTile, tiles, boardSize);

        bool NewPath(ITile targetTile, ITile startTile, ITile[] tiles, int boardSize)
        {
            if (startTile == targetTile || !targetTile.IsOpen) return true;
            List<Node> nodes = new List<Node>();
            Node targetNode = new Node(targetTile, startTile, targetTile);
            AddAdjacentOpenTiles(startTile, targetNode);
            if (NoOpenNodes()) return false;
            Node currentNode = CheapestOpenNode();

            while (currentNode.T != targetTile)
            {
                if (currentNode.T == targetTile) return true;
                CloseNode(currentNode);
                AddAdjacentOpenTiles(currentNode.T, targetNode);
                if (NoOpenNodes()) return false;
                currentNode = CheapestOpenNode();
            }

            if (currentNode.T == targetTile) return true;

            return false;

            #region INTERNAL
            void AddAdjacentOpenTiles(ITile tile, Node target)
            {
                CheckTile(Vector2Int.up);
                CheckTile(Vector2Int.down);
                CheckTile(Vector2Int.left);
                CheckTile(Vector2Int.right);

                void CheckTile(Vector2Int v2)
                {
                    if (!IsInBounds(tile.Coord + v2)) return;
                    ITile newNodeTile = tiles[(tile.Coord + v2).Vec2ToInt(boardSize)];
                    if (!newNodeTile.IsOpen || AlreadyInNodes(newNodeTile)) return;
                    nodes.Add(new Node(newNodeTile, startTile, target.T));
                }

                bool AlreadyInNodes(ITile t)
                {
                    for (int i = 0; i < nodes.Count; i++) if (nodes[i].T == t) return true;
                    return false;
                }

                bool IsInBounds(Vector2Int v2) => v2.x >= 0 && v2.x < boardSize && v2.y >= 0 && v2.y < boardSize;
            }

            bool NoOpenNodes()
            {
                if (nodes.Count == 0) { Debug.Log("No Open Nodes"); return true; }
                for (int i = 0; i < nodes.Count; i++) if (!nodes[i].IsClosed) return false;
                Debug.Log("NoOpenNodes");
                return true;
            }

            void CloseNode(Node node) => node.IsClosed = true;

            Node CheapestOpenNode()
            {
                int lowestF = int.MaxValue;
                Node cheapestNode = nodes[0];

                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].F <= lowestF && !nodes[i].IsClosed)
                    {
                        if (nodes[i].F < lowestF) { cheapestNode = nodes[i]; lowestF = nodes[i].F; }
                        else if (nodes[i].H < cheapestNode.H) { cheapestNode = nodes[i]; lowestF = nodes[i].F; }
                    }
                }

                return cheapestNode;
            }

            #endregion
        }
    }
}

public class Node
{
    public int F => G + H;
    public int G;
    public int H;
    public ITile T;
    public bool IsClosed;

    public Node(ITile t, ITile start, ITile target)
    {
        T = t;
        G = 1 + Mathf.Abs(t.Coord.x - start.Coord.x) + Mathf.Abs(t.Coord.y - start.Coord.y);
        H = 1 + Mathf.Abs(t.Coord.x - target.Coord.x) + Mathf.Abs(t.Coord.y - target.Coord.y);
    }
}

public interface ITile
{
    public Vector2Int Coord { get; }

    public bool IsOpen { get; }
}