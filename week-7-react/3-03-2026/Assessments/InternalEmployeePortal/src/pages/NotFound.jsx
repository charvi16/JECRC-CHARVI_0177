// Fallback page for invalid routes (404)
export default function NotFound() {
  return (
    <div className="page">
      <div className="card">

        {/* Error code */}
        <h1>404</h1>

        {/* Message */}
        <p>Page not found</p>

      </div>
    </div>
  );
}