import { useState } from "react";
import { useLanguage } from "../context/LanguageContext";
import { translate } from "../utils/translator";
import "./TranslatorBox.css";

export default function TranslatorBox() {
  const { fromLang, toLang } = useLanguage();
  const [input, setInput] = useState("");

  return (
    <div className="translator-box">
      <input
        className="input"
        placeholder="Enter text"
        value={input}
        onChange={(e) => setInput(e.target.value)}
      />

      <div className="output">
        {translate(input, fromLang, toLang)}
      </div>
    </div>
  );
}