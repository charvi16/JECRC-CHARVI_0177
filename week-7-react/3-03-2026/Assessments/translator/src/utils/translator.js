import { phrases } from "../data/phrases";

export const translate = (text, from, to) => {
  const match = phrases.find(
    (p) => p[from].toLowerCase() === text.toLowerCase()
  );

  return match ? match[to] : "Not found";
};