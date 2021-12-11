import sys

def is_corner(row, col, dim: 'tuple[int, int]'):
    return row%(dim[0] - 1) == 0 and col%(dim[1] - 1) == 0

def is_edge(row, col, dim: 'tuple[int, int]'):
    edge = row%(dim[0]-1) == 0 or col%(dim[1] - 1) == 0
    not_corner = not is_corner(row, col, dim)
    return edge and not_corner

def is_low_point(row, col):
    corner = is_corner(row, col, (nrows, ncols))
    edge = is_edge(row, col, (nrows, ncols))
    num = matrix[row][col]
    cdir = 1 if col == 0 else -1
    rdir = 1 if row == 0 else -1
    if corner:
        neighbors = [(row+rdir, col), (row, col+cdir)]
        return neighbors, num < matrix[row][col+cdir] and num < matrix[row+rdir][col]
    elif edge:
        if row == 0 or row == nrows-1:
            neighbors = [(row+rdir, col), (row, col-1), (row, col+1)]
            return neighbors, num < matrix[row+rdir][col] and num < matrix[row][col-1] and num < matrix[row][col+1]
        else:
            neighbors = [(row-1, col), (row+1, col), (row, col+cdir)]
            return neighbors, num < matrix[row-1][col] and num < matrix[row+1][col] and num < matrix[row][col+cdir]
    else:
        u = matrix[row+1][col]
        d = matrix[row-1][col]
        l = matrix[row][col-1]
        r = matrix[row][col+1]
        neighbors = [(row+1, col), (row-1, col), (row, col-1), (row, col+1)]
        return neighbors, num < min([u, d, l, r])

def get_basin(row, col, basin: list):
    neighbors = is_low_point(row, col)[0]
    for nr, nc in neighbors:
        if matrix[nr][nc] < 9 and (nr, nc) not in basin:
            basin.append((nr, nc))
            get_basin(nr, nc, basin)
    return basin

lines = open(sys.argv[1]).read().splitlines()
matrix = [[int(x) for x in line] for line in lines]
nrows, ncols = len(matrix), len(matrix[0])

# Part 1
answer_1 = 0
lowest_points = []
for row in range(nrows):
    for col in range(ncols):
        smallest = is_low_point(row, col)[1]
        if smallest:
            lowest_points.append((row, col))
            answer_1 += matrix[row][col]+1
print("Part 1:", answer_1)

# Part 2
basins = []
for r, c in lowest_points:
    basin = []
    smallest = is_low_point(r, c)[1]
    if smallest:
        basin.append((r, c))
        get_basin(r, c, basin)
        basins.append(basin)
basins.sort(key=len, reverse=True)
answer_2 = len(basins[0])*len(basins[1])*len(basins[2])
print("Part 2:", answer_2)