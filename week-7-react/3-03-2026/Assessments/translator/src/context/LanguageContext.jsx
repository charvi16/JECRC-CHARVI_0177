import { createContext, useContext, useState } from "react";

const LanguageContext = createContext();

export const LanguageProvider = ({ children }) => {
  const [fromLang, setFromLang] = useState("en");
  const [toLang, setToLang] = useState("hi");

  return (
    <LanguageContext.Provider value={{ fromLang, toLang, setFromLang, setToLang }}>
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = () => useContext(LanguageContext);