<template>
  <v-layout class="home-background">
    <v-layout class="home-background-mask" align-center fill-height>
    <v-flex xs12 md6>
      <v-layout row wrap justify-end>
        <v-flex xs12 md10 ma-3 class="home-detail">
          <h1>あなたの経歴はこのアプリが証明します</h1>
        </v-flex>
      </v-layout>
      <v-layout row wrap justify-end>
        <v-flex xs12 md5 mx-3>
          <v-btn
            color="#122D38"
            block
            outline
            large
            @click="signup"
          >
            <strong>はじめる</strong>
          </v-btn>
        </v-flex>
      </v-layout>
    </v-flex>
    </v-layout>
  </v-layout>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  methods: {
    ...mapActions({
      signup: 'auth/signup'
    })
  }
}
</script>

<style scoped>
.home-background{
  background: url('~@/assets/app-image.png'), linear-gradient(#F4FFC7, #408C98, #0F2530);
  background-position: right;
  background-repeat: no-repeat;
}
.home-background-mask{
  height: 100%;
  background: rgba(255,255,255,0.3);
}
.home-detail{
  color: #122D38;
  white-space:pre-wrap;
}
</style>
