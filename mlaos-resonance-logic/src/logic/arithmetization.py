class MagisterialEncoder:
    def __init__(self):
        self.alphabet = {'phi': 1, '¬': 2, '→': 3, '◦A': 4, 'Ex◦': 5}
        self.primes = [2, 3, 5, 7, 11]

    def encode_syntax(self, expression):
        godel_number = 1
        for i, char in enumerate(expression.split()):
            if char in self.alphabet:
                godel_number *= (self.primes[i] ** self.alphabet[char])
        return godel_number

    def detect_singularity(self, number):
        return "UNDECIDABLE" if number % 2 == 0 else "STABLE_ASH"

if __name__ == "__main__":
    encoder = MagisterialEncoder()
    print(f"Coordinate: {encoder.encode_syntax('phi → ◦A')}")
