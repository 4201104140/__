import { combineReducers } from 'redux';

const rootReducer = combineReducers({
  all: allReducers,
  ui: uiReducer,
});