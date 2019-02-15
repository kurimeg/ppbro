const state = {
  issuers: []
}

const mutations = {
  setIssuers (state, issuers) {
    state.issuers = issuers
  }
}

const actions = {
  fetchIssuers ({ commit }, param) {
    const issuers = [
      { name: '法政大学', src: '' },
      { name: '株式会社システムコンサルタント', src: '' },
      { name: '情報処理推進機構', src: '' },
      { name: '情報処理推進機構', src: '' }
    ]
    commit('setIssuers', issuers)
  },
  clearIssuers ({ commit }) {
    commit('setIssuers', [])
  },
  requestCertificate ({ commit }) {
    return new Promise((resolve, reject) => {
      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
