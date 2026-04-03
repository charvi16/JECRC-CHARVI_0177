import { useLanguage } from "../context/LanguageContext";
import "./LanguageSelector.css";

export default function LanguageSelector() {
  const { fromLang, toLang, setFromLang, setToLang } = useLanguage();

  return (
    <div className="selector">
      <select value={fromLang} onChange={(e) => setFromLang(e.target.value)}>
        <option value="en">English</option>
        <option value="hi">Hindi</option>
        <option value="fr">French</option>
        <option value="kn">Kannada</option>
      </select>

      <select value={toLang} onChange={(e) => setToLang(e.target.value)}>
        <option value="en">English</option>
        <option value="hi">Hindi</option>
        <option value="fr">French</option>
        <option value="kn">Kannada</option>
      </select>
    </div>
  );
}