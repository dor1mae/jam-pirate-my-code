using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class TileChecker
{
    private List<Tile> _tileList;
    private int _tileYCount;
    private int _tileXCount;

    public TileChecker(Tile[,] tiles, int tileYCount, int tileXCount)
    {
        _tileList = tiles.Clone().ConvertTo<List<Tile>>();
        _tileYCount = tileYCount;
        _tileXCount = tileXCount;
    }

    public List<Tile> CheckStartTile(AbstractItem item)
    {
        for (int i = 0; i < _tileXCount; i++)
        {
            for (int j = 0; j < _tileYCount; j++)
            {
                if (!_tileList.Find(x => (x.X == j && x.Y == i)).IsBusy)
                {
                    var tempList = CheckTilesForItem(i, j, item); //Ячейка найдена, начинаем проходить вниз и вправо, чтобы собрать необходимое количество ячеек

                    if (tempList == null)
                    {
                        continue;
                    }
                    else
                    {
                        return tempList;
                    }
                }
            }
        }

        return null;
    }

    public List<Tile> CheckStartTile(AbstractItem item, Tile tile)
    {
        for (int i = tile.Y; i < _tileXCount; i++)
        {
            for (int j = tile.X; j < _tileYCount; j++)
            {
                if (!_tileList.Find(x => (x.X == j && x.Y == i)).IsBusy)
                {
                    var tempList = CheckTilesForItem(i, j, item); //Ячейка найдена, начинаем проходить вниз и вправо, чтобы собрать необходимое количество ячеек

                    if (tempList == null)
                    {
                        continue;
                    }
                    else
                    {
                        return tempList;
                    }
                }
            }
        }

        return null;
    }

    private List<Tile> CheckTilesForItem(int x, int y, AbstractItem item)
    {
        int tilesForItemX = 0; int tilesForItemY = 0;//переменные для подсчета подхолящих ячееек
        int i = 0; int j = 0;
        Tile tileY = null;

        List<Tile> tempList = new List<Tile>(); //массив с подходящими ячейками, который мы вернем, если все будет в порядке.

        for (j = x; j < _tileXCount; j++)
        {
            int tilesForItemYInColumn = 0;

            tilesForItemX++;

            for (i = y; i < _tileYCount; i++)
            {
                var tempTile = _tileList.Find(tile => (tile.X == i && tile.Y == j));
                if (tempTile.IsBusy && tempTile.Item != item)
                {
                    if (j == y)
                    {
                        //случай, когда и снизу, и справа закончились подходящие ячейки
                        if (tilesForItemX == item.XTileCount && item.YTileCount * item.XTileCount == tilesForItemY)
                        {
                            return tempList;
                        }
                        else return null;
                    }
                    else break;
                }
                else
                {
                    tilesForItemYInColumn++;
                    tilesForItemY++;

                    tileY = _tileList.Find(tile => (tile.X == i && tile.Y == j)); //находим ячейку с массиве всех ячеек
                    if (!tempList.Contains(tileY)) //есть ли она уже в списке для возврата?
                    {
                        tempList.Add(tileY);
                    }

                    if (tilesForItemYInColumn == item.YTileCount)
                    {
                        if (tilesForItemX == item.XTileCount && item.YTileCount * item.XTileCount == tilesForItemY) // Хватает ли уже ячеек?
                        {
                            return tempList;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        return null;
    }
}
