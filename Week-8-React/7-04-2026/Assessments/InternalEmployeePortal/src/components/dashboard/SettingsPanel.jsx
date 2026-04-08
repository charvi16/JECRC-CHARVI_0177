import useTheme from "../../hooks/useTheme";

export default function SettingsPanel() {
  const { theme } = useTheme();

  return (
    <div className="card">
      <h3>Settings</h3>
      <p>Current Theme: {theme}</p>
    </div>
  );
}
