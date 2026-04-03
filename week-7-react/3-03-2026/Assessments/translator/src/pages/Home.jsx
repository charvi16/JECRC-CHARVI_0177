import TranslatorBox from "../components/TranslatorBox";
import LanguageSelector from "../components/LanguageSelector";
import "./Home.css";

export default function Home() {
  return (
    <div className="home">
      <div className="hero">
        <h1>Learn Languages Smarter 🚀</h1>
        <p>Translate, practice, and master multiple languages in one place</p>
      </div>

      <div className="card">
        <LanguageSelector />
        <TranslatorBox />
      </div>
    </div>
  );
}