const logger = (store) => (next) => (action) => {
  console.group(`Redux Action: ${action.type}`);
  console.log("Previous State:", store.getState());
  console.log("Action:", action);
  const result = next(action);
  console.log("Next State:", store.getState());
  console.groupEnd();
  return result;
};

export default logger;