<template>
  <v-flex xs12 md6 class="certificate-container">
    <!-- <v-img
      class="certificate"
      :src="certificateSrc"
      position="left"
      contain
    ></v-img> -->
    <v-card height="17vh" hover flat class="certificate">
      <v-layout row wrap mt-2 mx-2>
        <v-flex xs3 md3 pa-2>
          <v-img
            :src="!this.certificate.src ? require('@/assets/university.svg') : this.certificate.src"
            position="left"
            contain
          ></v-img>
        </v-flex>
        <v-flex xs7 md8 pa-2>
            {{ certificate.issuerName }}<br/>
            {{ certificate.detail + ' ' + certificate.type }}
        </v-flex>
      </v-layout>
      <!-- <v-layout>
        <v-flex offset-xs7 xs4>
          <v-chip :color="certificate.issuerSign ? 'accent': 'secondary'" text-color="white">
            <v-avatar>
              <v-icon>check_circle</v-icon>
            </v-avatar>
            {{ certificate.issuerSign ? '取得済': '要求中' }}
          </v-chip>
        </v-flex>
      </v-layout> -->
    </v-card>
    <v-flex v-if="certificate.issuerSign" offset-xs10 xs2 class="certificate-badge">
      <v-icon color="#FF8160" size="50">fas fa-award</v-icon>
    </v-flex>
  </v-flex>
  <!-- <v-card color="white" class="mb-3">
    <v-layout row wrap>
      <v-flex xs4 md2>
        <v-img
          :src="certificateSrc"
          position="left"
          contain
        ></v-img>
      </v-flex>
      <v-flex xs8 md10>
        <v-card-text class="px-2">
          {{ certificate.issuerName }}<br/>
          {{ certificate.detail + ' ' + certificate.type }}
        </v-card-text>
      </v-flex>
    </v-layout>
  </v-card> -->
</template>

<script>
export default {
  props: {
    certificate: Object
  }
}
</script>

<style scoped>
.certificate{
  border: solid 1px #489DAB;
  box-shadow:
    0px 0px 0px 0px rgba(0,0,0,0.2),
    0px 0px 0px 0px rgba(0,0,0,0.14),
    0px 0px 0px 0px rgba(0,0,0,0.12);
}
.certificate-container{
  position: relative;
  /* border: solid 1px #489DAB; */
}
.certificate-badge{
  position: absolute;
  top:0;
}
</style>
