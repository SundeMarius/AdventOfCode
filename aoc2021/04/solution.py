#!/usr/bin/python3
import sys

class BingoNumber:
    def __init__(self, number: int) -> None:
        self.number = number
        self.selected = False

    def check(self, number: int) -> None:
        if self.number == number:
            self.selected = True

    def is_selected(self) -> bool:
        return self.selected

    def value(self) -> int:
        return self.number

    def __repr__(self) -> str:
        return str(self.number) + "," + ("True" if self.selected else "False")

class BingoBoard:
    def __init__(self, board_as_str: str) -> None:
        self.board: list[list[BingoNumber]] = []
        rows = board_as_str.split('\n')
        for row in rows:
            self.board.append([BingoNumber(int(x)) for x in row.split()])

    def apply_bingo_number(self, bingo_number: int) -> None:
        for row in self.board:
            for n in row:
                n.check(bingo_number)

    def bingo(self) -> bool:
        # Check rows
        for row in self.board:
            if all(n.is_selected() for n in row):
                return True 
        # Check columns
        for col_idx in range(5):
            column = [row[col_idx] for row in self.board]
            if all(n.is_selected() for n in column):
                return True
        return False
    
    def score(self, winning_number: int) -> int:
        score = 0
        for row in self.board:
            for n in row:
                if not n.is_selected():
                    score += n.value()
        score *= winning_number
        return score

    def __repr__(self) -> str:
        ret = ""
        for row in self.board:
            ret += "    ".join([str(x) for x in row]) + "\n"
        return ret

def create_boards(boards_as_str):
    bingo_boards = []
    for board_str in boards_as_str:
        board = BingoBoard(board_str) 
        bingo_boards.append(board)
    return bingo_boards

def part1(bingo_boards, bingo_numbers):
    for number in bingo_numbers:
        for board in bingo_boards:
            board.apply_bingo_number(number)
            if board.bingo():
                print("--- Bingo! ---\n")
                print(board)
                print("Score: %i" % board.score(number))
                return

def part2(bingo_boards: list, bingo_numbers):
    last_board = []
    for number in bingo_numbers:
        for board in bingo_boards:
            board.apply_bingo_number(number)                   
        for board in bingo_boards:                
            if board.bingo(): 
                last_board = board
                bingo_boards.remove(board)
        if not bingo_boards:
            print("--- Last bingo! ---\n")
            print(last_board)
            print("Score: %i" % last_board.score(number))
            return


def main():
    with open(sys.argv[1]) as f:
        content = f.read().split("\n\n")

        # Get bingo numbers
        bingo_numbers = content.pop(0)
        bingo_numbers = bingo_numbers.split(',')
        bingo_numbers = [int(n) for n in bingo_numbers]

        # PART I: Run through bingo numbers
        # Fill boards
        bingo_boards = create_boards(content)
        part1(bingo_boards, bingo_numbers)
        
        # PART II: Find the board that wins last
        # Fill boards
        bingo_boards = create_boards(content)
        part2(bingo_boards, bingo_numbers)

if __name__ == "__main__":
    main()
