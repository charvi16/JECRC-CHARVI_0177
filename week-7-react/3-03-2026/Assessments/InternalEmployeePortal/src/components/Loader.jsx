// Loader component
// This component is displayed during async operations like API calls, login, or data fetching
// It improves user experience by indicating that something is loading
export default function Loader() {
  return (
    // The "loader" class can be styled (spinner, animation, etc.) in CSS
    <div className="loader">
      Loading...
    </div>
  );
}