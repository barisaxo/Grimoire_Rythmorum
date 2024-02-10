using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    public static Vector2Int[] NewSailingPath(
        this Vector2Int targetCoord,
        Vector2Int startCoord,
        Sea.Cell[] cells,
        int AreaSize
        )
    {
        if (startCoord == targetCoord) { return new Vector2Int[] { }; }

        Vector2Int[] blockedCoords = ConsolidateBlockedCoords(cells);

        List<Vector2Int> path = new();
        List<NodeV2I> nodes = new();

        NodeV2I startNode = new(startCoord, startCoord, targetCoord);
        NodeV2I target = new(targetCoord, startCoord, targetCoord);

        if (IsInBounds(targetCoord) && !IsBlocked(targetCoord, blockedCoords))
        {
            nodes.Add(new NodeV2I(targetCoord, startCoord, targetCoord));
            AddAdjacentOpenCoords(startCoord, target);
        }

        if (NoOpenNodes()) return new Vector2Int[] { };

        NodeV2I currentNode = CheapestOpenNode();
        NodeV2I prevNode = currentNode;

        while (currentNode.Coord != target.Coord)
        {
            CloseNode(currentNode);
            AddAdjacentOpenCoords(currentNode.Coord, target);
            if (NoOpenNodes()) return new Vector2Int[] { };
            prevNode = currentNode;
            currentNode = CheapestOpenNode();
        }

        if (!path.Contains(target.Coord) && !IsBlocked(target.Coord, blockedCoords)) path.Add(target.Coord);

        target = prevNode;

        while (target.Coord != startNode.Coord)
        {
            nodes.Clear();
            currentNode = startNode;

            while (currentNode.Coord != target.Coord)
            {
                CloseNode(currentNode);
                AddAdjacentOpenCoords(currentNode.Coord, target);
                if (NoOpenNodes()) return new Vector2Int[] { };
                prevNode = currentNode;
                currentNode = CheapestOpenNode();
            }

            if (!path.Contains(target.Coord) && !IsBlocked(target.Coord, blockedCoords)) path.Add(target.Coord);
            target = prevNode;
        }

        return path.ToArray();

        #region  INTERNAL
        void AddAdjacentOpenCoords(Vector2Int Coord, NodeV2I target)
        {
            CheckCoord(Vector2Int.up + Coord);
            CheckCoord(Vector2Int.down + Coord);
            CheckCoord(Vector2Int.left + Coord);
            CheckCoord(Vector2Int.right + Coord);

            void CheckCoord(Vector2Int potentialNode)
            {
                if (!IsInBounds(potentialNode)) return;
                if (IsBlocked(potentialNode, blockedCoords)) return;
                if (AlreadyInNodes(potentialNode)) return;
                nodes.Add(new NodeV2I(potentialNode, startCoord, targetCoord));
            }
        }

        bool AlreadyInNodes(Vector2Int v2i)
        {
            foreach (NodeV2I n in nodes)
                if (n.Coord == v2i) return true;
            return false;
        }

        bool IsInBounds(Vector2Int v2i) => v2i.x >= 0 && v2i.x < AreaSize && v2i.y >= 0 && v2i.y < AreaSize;

        Vector2Int[] ConsolidateBlockedCoords(Sea.Cell[] tiles)
        {
            List<Vector2Int> v2is = new();
            foreach (Sea.Cell cell in tiles) if (cell.IsAStarOpen) v2is.Add(cell.Coord);
            return v2is.ToArray();
        }

        bool IsBlocked(Vector2Int v2i, Vector2Int[] blockedCoords)
        {
            foreach (Vector2 coord in blockedCoords)
                if (coord == v2i) return true;
            return false;
        }

        bool NoOpenNodes()
        {
            if (nodes.Count == 0) return true;
            foreach (NodeV2I n in nodes) if (!n.IsClosed) return false;
            return true;
        }

        NodeV2I CheapestOpenNode()
        {
            int lowestF = int.MaxValue;
            NodeV2I cheapestNode = nodes[0];

            foreach (NodeV2I n in nodes)
            {
                if (n.F <= lowestF && !n.IsClosed)
                {
                    if (n.F < lowestF) { cheapestNode = n; lowestF = n.F; }
                    else if (n.H < cheapestNode.H) { cheapestNode = n; lowestF = n.F; }
                }
            }
            return cheapestNode;
        }

        void CloseNode(NodeV2I node) { node.IsClosed = true; }
        #endregion INTERNAL
    }



    public static ITile GetTile(this ITile[] tiles, Vector2Int v2i)
    {
        foreach (ITile tile in tiles)
            if (v2i == tile.Coord) return tile;

        return null;
    }

    public static ITile[] NewSailingPath(
        this ITile targetTile,
        ITile startTile,
        ITile[] tiles,
        int boardSize)
    {
        if (startTile == targetTile) { return new ITile[] { }; }

        List<ITile> path = new();
        List<Node> nodes = new();
        Node startNode = new(startTile, startTile, targetTile);
        Node target = new(targetTile, startTile, targetTile);

        if (targetTile.IsAStarOpen) path.Add(targetTile);
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

        if (!path.Contains(target.T) && target.T.IsAStarOpen) path.Add(target.T);

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

            if (!path.Contains(target.T) && target.T.IsAStarOpen) path.Add(target.T);
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
                if (!t.IsAStarOpen) return;
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
            if (startTile == targetTile || !targetTile.IsAStarOpen) return true;
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
                    if (tiles[(tile.Coord + v2).Vec2ToInt(boardSize)] == null) return;
                    ITile newNodeTile = tiles[(tile.Coord + v2).Vec2ToInt(boardSize)];
                    if (!newNodeTile.IsAStarOpen || AlreadyInNodes(newNodeTile)) return;
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

public class NodeV2I
{
    public int F => G + H;
    public int G;
    public int H;
    public Vector2Int Coord;
    public bool IsClosed;

    public NodeV2I(Vector2Int v2i, Vector2Int start, Vector2Int target)
    {
        Coord = v2i;
        G = 1 + Mathf.Abs(v2i.x - start.x) + Mathf.Abs(v2i.y - start.y);
        H = 1 + Mathf.Abs(v2i.x - target.x) + Mathf.Abs(v2i.y - target.y);
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

    public bool IsAStarOpen { get; }
}