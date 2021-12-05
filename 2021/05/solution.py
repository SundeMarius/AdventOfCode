import sys

class Point2D:
    def __init__(self, x, y):
        self.x: int = x
        self.y: int = y
        self.touched: int = 1

    def touch(self):
        self.touched += 1

    def __repr__(self):
        return str(self.x) + "," + str(self.y) + "," + str(self.touched)

def check_point(point: Point2D, points: list[Point2D]):
    for p in points:
        if p.x == point.x and p.y == point.y:
            p.touch()
            return
    points.append(point)

def count_overlapping_points(points):
    return sum(1 for point in points if point.touched >= 2)

def get_input(part: int):
    lines = open(sys.argv[1]).read().splitlines()
    start_points = []
    end_points = []
    for line in lines:
        line = line.split(" -> ")
        start = tuple(map(int, line[0].split(',')))
        end = tuple(map(int, line[1].split(',')))
        condition1 = start[0] == end[0] or start[1] == end[1]
        condition2 = abs(end[1] - start[1]) == abs(end[0] - start[0])
        if condition1: 
            start_points.append(Point2D(*start))
            end_points.append(Point2D(*end))
        if part == 2:
            if condition2:
                start_points.append(Point2D(*start))
                end_points.append(Point2D(*end))
    return start_points, end_points


def part1():
    # Parse input
    start_points, end_points = get_input(1)
    points = []
    for start, end in zip(start_points, end_points):
        if start.x == end.x:
            direction = (1 if end.y > start.y else -1)
            for y in range(start.y, end.y+direction, direction):
                point = Point2D(start.x, y)
                check_point(point, points)
        else:
            direction = (1 if end.x > start.x else -1)
            for x in range(start.x, end.x+direction, direction):
                point = Point2D(x, end.y)
                check_point(point, points)
    print("Number of points with at least two lines overlap: ", count_overlapping_points(points))

def part2():
    # Parse input
    start_points, end_points = get_input(2)
    points = []
    for start, end in zip(start_points, end_points):
        if start.x == end.x:
            direction = (1 if end.y > start.y else -1)
            for y in range(start.y, end.y+direction, direction):
                point = Point2D(start.x, y)
                check_point(point, points)
        elif start.y == end.y:
            direction = (1 if end.x > start.x else -1)
            for x in range(start.x, end.x+direction, direction):
                point = Point2D(x, end.y)
                check_point(point, points)
        else:
            dir_x = (1 if end.x > start.x else -1)
            dir_y = (1 if end.y > start.y else -1)
            i = 0
            for x in range(start.x, end.x+dir_x, dir_x):
                y = start.y + dir_y*i
                point = Point2D(x, y)
                check_point(point, points)
                i += 1
    print("Number of points with at least two lines overlap: ", count_overlapping_points(points))

print("Part 1:")
part1()
print("Part 2:")
part2()
