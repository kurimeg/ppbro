<template>
  <v-layout class="home-background" align-center justify-center fill-height>
    <v-flex xs12>
      <!-- <v-layout row wrap justify-center>
        <v-flex xs12 md6 ma-3 class="home-detail">
          <h1>あなたの経歴はこのアプリが証明します</h1>
        </v-flex>
      </v-layout> -->
      <v-layout row wrap justify-center>
        <v-flex xs12 md3 mx-3>
          <v-text-field
            color="white"
            v-model="profile.name"
            label="氏名"
            required
            outline
          ></v-text-field>
        </v-flex>
        <v-flex xs12 md3 mx-3>
          <v-text-field
            color="white"
            v-model="profile.mail"
            label="メールアドレス"
            required
            outline
            type="email"
          ></v-text-field>
        </v-flex>
      </v-layout>
      <v-layout row wrap justify-center>
        <v-flex xs12 md3 mx-3>
          <v-btn
            color="white"
            outline
            block
            large
            @click="signup(profile)"
          >
            <strong>はじめる</strong>
          </v-btn>
        </v-flex>
      </v-layout>
    </v-flex>
    <div class="home-background-image">
    </div>
  </v-layout>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  data: function () {
    return {
      profile: {
        name: '',
        mail: ''
      }
    }
  },
  methods: {
    ...mapActions({
      signup: 'auth/signup'
    })
  }
}
</script>

<style scoped>
.home-background{
  position: relative;
  background: linear-gradient(#F4FFC7, #408C98, #0F2530);
}
.home-background-image{
  /* position: absolute;
  z-index: 2;
  background-image: url('~@/assets/app-image.png'); */
}
.home-detail{
  color: #FFFFFF;
  white-space:pre-wrap;
}
</style>
