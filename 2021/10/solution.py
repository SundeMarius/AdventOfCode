import sys
brackets = {'(': ')', '[': ']', '{': '}', '<': '>'}
points_1 = {')':3, ']':57, '}':1197, '>':25137}
points_2 = {')':1, ']':2, '}':3, '>':4}

lines = open(sys.argv[1]).read().splitlines()

def legal_chunk(chunk: str):
    for i in range(len(chunk)):
        if chunk[i] in brackets.values():
            if brackets[chunk[i-1]] == chunk[i]:
                return legal_chunk(chunk[:i-1] + chunk[i+1:])
            else:
                return chunk[i], False
    return ''.join(brackets[c] for c in chunk[::-1]), True

# Part 1
score = 0
completion_strings = []
for line in lines:
    output, legal = legal_chunk(line)
    if legal:
        completion_strings.append(output)
    else:
        score += points_1[output]
print("Part 1:", score)

# Part 2
scores = []
for line in completion_strings:
    score = 0
    for c in line:
        score = 5*score + points_2[c]
    scores.append(score)
scores.sort()
print("Part 2:", scores[len(scores)//2])