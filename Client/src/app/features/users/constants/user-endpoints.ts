export const USER_ENDPOINTS = {
  GET_USERS_V1: 'v1/users',
  COUNT_USERS_V1: 'v1/users/count',
  SEARCH_USERS_V1: 'v1/users/search',
  GET_BY_ID_USER_V1: (id: number) => `v1/users/${id}`,
  CREATE_USERS_V1: 'v1/users',
  UPDATE_USERS_V1: 'v1/users',
  DELETE_BY_ID_USER_V1: (id: number) => `v1/users/${id}`,
};