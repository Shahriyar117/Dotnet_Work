const reducer = (state, action) => {
  switch (action.type) {
    case "GET_LOADING":
      return {
        ...state,
        loading: false,
      };
    case "SET_FORCASTS":
      return {
        ...state,
        foreCasts: action.payload,
      };
    case "GET_DEPARTMENT":
      return {
        ...state,
        department: action.payload,
      };
    case "UPDATE_DEPARTMENT":
      return {
        ...state,
        department: action.payload,
      };
    case "SET_TOGGLE":
      return {
        ...state,
        toggle: action.payload,
      };
  }
  return <div>state</div>;
};
export default reducer;
