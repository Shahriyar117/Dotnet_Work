const reducer = (state, action) => {
  switch (action.type) {
    case "GET_LOADING":
      return {
        ...state,
        loading: false,
      };
    case "GET_EMPLOYEE":
      return {
        ...state,
        employee: action.payload,
      };
    case "UPDATE_EMPLOYEE":
      return {
        ...state,
        employee: action.payload,
      };
  }
  return <div>state</div>;
};
export default reducer;
