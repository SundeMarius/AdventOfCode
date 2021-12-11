import sys

lines = open(sys.argv[1]).read().splitlines()

# Part 1
# Create dictionary to keep count of 1, 4, 7 and 8
digits = {1:0, 4:0, 7:0, 8:0}
for line in [s.split(' | ')[1] for s in lines]:
    codes = line.split(' ')
    for code in codes:
        nletters = len(code)
        if nletters == 2:
            digits[1] += 1
        elif nletters == 4:
            digits[4] += 1
        elif nletters == 3:
            digits[7] += 1
        elif nletters == 7:
            digits[8] += 1
print(f"Part 1: {sum(digits.values())}")

def decoder(codes: "list[str]") -> 'dict[str]':
    sorted_codes = sorted(codes, key=len)
    decoded_digits = {}
    memory = {}
    sets = [set(list(s)) for s in sorted_codes]
    for code, char_set in zip(sorted_codes, sets):
        if len(code) == 2:
            memory[1] = char_set
            decoded_digits[code] = 1
        elif len(code) == 3:
            decoded_digits[code] = 7
        elif len(code) == 4:
            memory[4] = char_set
            decoded_digits[code] = 4
        elif len(code) == 5:
            if len(char_set - memory[1]) == 3:
                decoded_digits[code] = 3
            elif len(char_set - memory[4]) == 3:
                decoded_digits[code] = 2
            else:
                decoded_digits[code] = 5
        elif len(code) == 6:
            if len(char_set - memory[4]) == 2:
                decoded_digits[code] = 9
            elif len(char_set - memory[1]) == 5:
                decoded_digits[code] = 6
            else:
                decoded_digits[code] = 0
        else:
            decoded_digits[code] = 8
    return decoded_digits

# Part 2
# Decode and runt through output lines
summ = 0
for line in lines:
    output_digits = ''
    inp, out = line.split(' | ')
    in_codes = [''.join(sorted(s)) for s in inp.split(' ')]
    out_codes = [''.join(sorted(s)) for s in out.split(' ')]
    decoded_digits = decoder(in_codes)
    for code in out_codes:
        output_digits += str(decoded_digits[code])
    summ += int(output_digits)
print(f"Part 2: {summ}")

    