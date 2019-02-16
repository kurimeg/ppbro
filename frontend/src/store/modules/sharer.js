const state = {
  sharers: []
}

const mutations = {
  setSharers (state, sharers) {
    state.sharers = sharers
  }
}

const actions = {
  fetchSharers ({ commit }, param) {
    const issuers = [
      { name: '株式会社システムコンサルタント', src: '' },
      { name: 'System Consultant Information India (P) Ltd.', src: '' }
    ]
    commit('setSharers', issuers)
  },
  clearSharers ({ commit }) {
    commit('setSharers', [])
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
